using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BridgeASMX
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private ComplexTypeInfo[] _ComplexTypes;
        private MethodInfo[] _Methods;

        private string _OutputCode;

        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            try
            {
                var parser = new WSDLParser(txtServicePath.Text.Contains("?wsdl") ? txtServicePath.Text : txtServicePath.Text + "?wsdl");

                _ComplexTypes = parser.GetComplexTyps();
                BindComplexTypeInfosToListBox();


                _Methods = parser.GetMethods();
                BindMethodsToListBox();

                btnSaveFile.Enabled = true;
                btnPreview.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry I can't analyse.\r\n" + "Message:\r\n" + ex.Message, "Exception Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BindMethodsToListBox()
        {
            chblMethods.Items.Clear();
            chblMethods.DataSource = null;
            foreach (var method in _Methods)
            {
                chblMethods.DisplayMember = "MethodName";
                chblMethods.Items.Add(method, true);
            }
        }

        private void BindComplexTypeInfosToListBox()
        {
            lbModels.Items.Clear();
            lbModels.DataSource = null;
            lbModels.DataSource = _ComplexTypes;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void GenCode()
        {
            // Declation
            var strDeclear = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "template\\" + "DECLEAR");
            _OutputCode += strDeclear;


            // Start Object Models
            _OutputCode += "//---------------------------------------------------------------------------//" + Environment.NewLine;
            _OutputCode += "//-----------------------------Object Models--------------------------------//" + Environment.NewLine;
            _OutputCode += "//--------------------------------------------------------------------------//" + Environment.NewLine + Environment.NewLine;


            for (var i = 0; i < lbModels.Items.Count; i++)
            {


                var t = lbModels.Items[i] as ComplexTypeInfo;
                if (t.Name.IndexOf("ArrayOf", StringComparison.Ordinal) > -1)
                {
                    _OutputCode += "// -- " + t.Name.Replace("ArrayOf", "") + "s" + Environment.NewLine;
                    _OutputCode += "var " + t.Name.Replace("ArrayOf", "") + "s=new Array();" + Environment.NewLine;
                    _OutputCode += Environment.NewLine;
                }
                else
                {
                    _OutputCode += "// -- " + t.Name + Environment.NewLine;
                    _OutputCode += "function " + t.Name + "(){" + Environment.NewLine;
                    _OutputCode += "  " + "/// <summary>" + t.Name + "</summary>" + Environment.NewLine;

                    foreach (var para in t.Parameters)
                    {
                        if (para.Type.Contains("ArrayOf"))
                        {
                            _OutputCode += "  " + t.Name + ".prototype." + para.Name + "=new Array();" + Environment.NewLine;
                        }
                        else
                        {
                            _OutputCode += "  " + t.Name + ".prototype." + para.Name + "='';" + Environment.NewLine;
                        }
                    }
                    _OutputCode += "}" + Environment.NewLine + Environment.NewLine;


                }

            }
            _OutputCode += Environment.NewLine + Environment.NewLine;


            //Methods

            _OutputCode += "//-----------------------------Methods---------------------------------//" + Environment.NewLine + Environment.NewLine;

            // foreach (var m in _Methods)
            for (var i = 0; i < chblMethods.Items.Count; i++)
            {

                if (chblMethods.GetItemChecked(i))
                {
                    var m = chblMethods.Items[i] as MethodInfo;

                    _OutputCode += "// -- " + m.MethodName + Environment.NewLine;

                    var template = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "\\template\\METHOD");
                    var iparas = string.Join(",", (from p in m.InputParameters select p.Name).ToArray());
                    if (!string.IsNullOrEmpty(iparas))
                    {
                        iparas = iparas + ",";
                    }
                    template = template.Replace("#NAME#", m.MethodName).Replace("#DESC#", m.Desc).Replace("#IPARAS#", iparas);

                    //處理傳入
                    if (m.InputParameters.Length > 0)
                    {
                        var iParams = "";
                        List<string> iDatas = new List<string>();

                        foreach (var paras in m.InputParameters)
                        {
                            iParams += "    " + "/// <param name=\"" + paras.Name + "\" type=\"" + paras.Type + "\">" +
                                       paras.Name + "</param>" + Environment.NewLine;
                            iDatas.Add(paras.Name + ":\"" + "+JSON.stringify(" + paras.Name + ")");
                        }
                        var iData = "\"{";

                        foreach (var data in iDatas)
                        {
                            iData += data + "+\",";
                        }
                        iData = iData.Substring(0, iData.Length - 2);
                        iData += "\"}\"";
                        template = template.Replace("#IPARASCOMMENTS#", iParams);
                        template = template.Replace("#RDATAS#", iData);
                    }
                    else
                    {
                        template = template.Replace("#IPARASCOMMENTS#", "");
                        template = template.Replace("#RDATAS#", "''");
                    }


                    //處理傳出
                    if (m.OutParameters.Length > 0)
                    {
                        string typeResult = m.OutParameters[0].Type;
                        if (typeResult.Contains("ArrayOf"))
                        {
                            typeResult = typeResult.Replace("ArrayOf", "") + "s";
                            template = template.Replace("#RETURN#", "new Array()");
                        }
                        else
                        {
                            var isBasic = true;
                            foreach (var t in _ComplexTypes)
                            {
                                if (m.OutParameters[0].Type == t.Name)
                                {
                                    if (t.Name.Contains("ArrayOf"))
                                    {
                                        template = template.Replace("#RETURN#", "new Array()");
                                        isBasic = false;
                                        break;
                                    }
                                    template = template.Replace("#RETURN#", "new " + t.Name + "()");
                                    isBasic = false;
                                    break;
                                }
                            }

                            if (isBasic)
                            {
                                template = template.Replace("#RETURN#", "''");
                            }
                        }
                        template = template.Replace("#RTYPE#", typeResult);
                        template = template.Replace("#RVALUENAME#", m.OutParameters[0].Name + " as " + typeResult);
                    }
                    else
                    {
                        template = template.Replace("#RTYPE#", "void");
                        template = template.Replace("#RVALUENAME#", "No return data.");
                        template = template.Replace("#RETURN#", "null");
                    }

                    template = template.Replace("#PATH#", txtOutLocation.Text);
                    _OutputCode += template + Environment.NewLine + Environment.NewLine;
                }

            }

        }

        private void btnPreview_Click(object sender, EventArgs e)
        {

            if (_ComplexTypes == null && _Methods == null)
            {
                MessageBox.Show("Sorry , No data to Convert.");
                return;
            }

            //Generate code.
            GenCode();

            // Output to preview.
            var frmShowCode = new frmShowCode(_OutputCode);
            frmShowCode.ShowDialog();

        }

        private void txtServicePath_TextChanged(object sender, EventArgs e)
        {
            txtOutLocation.Text = txtServicePath.Text;
        }

        private void btnSaveFile_Click(object sender, EventArgs e)
        {

            //Generate code.
            GenCode();


            var sfd = new SaveFileDialog();
            sfd.Filter = "Javscript files (*.js)|*.js|All files (*.*)|*.*";
            sfd.ShowDialog();

            if (!string.IsNullOrEmpty(sfd.FileName))
            {
                File.WriteAllText(sfd.FileName, _OutputCode);
            }

        }


    }
}
