using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace WebConfigTransformTest.Controllers
{
    public class ValuesController : ApiController
    {
        private XmlDocument _configXmlDoc;
        // GET api/values
        public IEnumerable<string> Get()
        {
            var webConfigContent = string.Empty;
            var doc = new XmlDocument();
            doc.Load("web.config");

            using (var stringWriter = new StringWriter())
            {
                using (var xmlTextWriter = XmlWriter.Create(stringWriter))
                {
                    doc.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    webConfigContent = stringWriter.GetStringBuilder().ToString();
                }
            }

            var result = new List<string> {
                $"key1: {ConfigurationManager.AppSettings["key1"]}",

                $"key2.key: {ConfigurationManager.AppSettings["key2.key"]}",

                $"conn1: {ConfigurationManager.ConnectionStrings["conn1"].ConnectionString}",

                $"conn3.conn: {ConfigurationManager.ConnectionStrings["conn3.conn"].ConnectionString}",

                $"conn4: {ConfigurationManager.ConnectionStrings["conn4"].ConnectionString}",

                $"xmlContent: {webConfigContent}",

        };

            return result;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
