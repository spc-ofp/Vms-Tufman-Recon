using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NVelocity.App;
using NVelocity;
using NVelocity.Runtime;
using System.Web.Hosting;


namespace Recon.Services
{
    public class TemplatingService
    {
        private readonly VelocityEngine _engine;

        public TemplatingService()
        {
            _engine = new VelocityEngine();
            _engine.SetProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, Path.Combine(HostingEnvironment.ApplicationPhysicalPath, "App_Data\\Templates"));
            _engine.Init();
        }

        public String GetTemplatedDocument(String templateName, IDictionary<String, String> data)
        {
            Template template = _engine.GetTemplate(templateName);
            VelocityContext context = new VelocityContext();
            data.Keys.ToList().ForEach(k => context.Put(k, data[k]));
            StringWriter writer = new StringWriter();
            template.Merge(context, writer);
            return writer.ToString();
        }
    }
}
