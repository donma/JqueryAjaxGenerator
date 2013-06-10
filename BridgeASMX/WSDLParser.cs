using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Services.Description;
using System.Xml.Schema;

namespace BridgeASMX
{
    public class WSDLParser
    {
        private ServiceDescription _ServiceDescription;

        public WSDLParser(string servicePath)
        {
            var wc = new WebClient();
            wc.Encoding = UTF8Encoding.UTF8;
            byte[] byteArray = Encoding.UTF8.GetBytes(wc.DownloadString(servicePath));
            var stream = new MemoryStream(byteArray);

            _ServiceDescription = ServiceDescription.Read(stream);
        }

        public ComplexTypeInfo[] GetComplexTyps()
        {
            var res = new List<ComplexTypeInfo>();
            Types types = _ServiceDescription.Types;
            XmlSchema xmlSchema = types.Schemas[0];

            foreach (XmlSchemaObject item in xmlSchema.Items)
            {
                var elems = item as XmlSchemaComplexType;
                if (elems != null)
                {
                    var entity = new ComplexTypeInfo { Name = elems.Name, Parameters = new List<Parameter>() };
                    var particle = elems.Particle;
                    var sequence = particle as XmlSchemaSequence;
                    if (sequence != null)
                    {
                        foreach (XmlSchemaElement childElement in sequence.Items)
                        {
                            string parameterName = childElement.Name;
                            string parameterType = childElement.SchemaTypeName.Name;
                            entity.Parameters.Add(new Parameter(parameterName, parameterType));
                        }
                    }

                    res.Add(entity);
                }
            }
            return res.ToArray();
        }

        public MethodInfo[] GetMethods()
        {
            var res = new List<MethodInfo>();

            foreach (PortType portType in _ServiceDescription.PortTypes)
            {
                foreach (Operation operation in portType.Operations)
                {

                    var me = new MethodInfo();
                    me.MethodName = operation.Name;
                    me.InputMessageName = operation.Messages.Input.Message.Name;
                    me.OutputMessageName = operation.Messages.Output.Message.Name;


                    me.InputParameters = GetParameters(_ServiceDescription, _ServiceDescription.Messages[me.InputMessageName].Parts[0].Element.Name);
                    me.OutParameters = GetParameters(_ServiceDescription, _ServiceDescription.Messages[me.OutputMessageName].Parts[0].Element.Name);

                    if (operation.DocumentationElement != null)
                        me.Desc = operation.DocumentationElement.InnerText;

                    res.Add(me);
                }
            }


            return res.ToArray();
        }

        private static Parameter[] GetParameters(ServiceDescription serviceDescription, string messagePartName)
        {
            Types types = serviceDescription.Types;
            XmlSchema xmlSchema = types.Schemas[0];
            return (from sequence in (from schemaElement in xmlSchema.Items.OfType<XmlSchemaElement>() where schemaElement.Name == messagePartName select schemaElement.SchemaType).OfType<XmlSchemaComplexType>().Select(complexType => complexType.Particle).OfType<XmlSchemaSequence>() from XmlSchemaElement childElement in sequence.Items let parameterName = childElement.Name let parameterType = childElement.SchemaTypeName.Name select new Parameter(parameterName, parameterType)).ToArray();
        }
    }

}
