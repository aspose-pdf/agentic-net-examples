using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main(string[] args)
    {
        const string htmlPath = "sample.html";
        const string outputPdf = "output.pdf";

        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine("HTML file not found: " + htmlPath);
            return;
        }

        // Configuration switch: set environment variable DISABLE_BASEURL=1 to disable Base URL injection
        string disableBaseUrlEnv = Environment.GetEnvironmentVariable("DISABLE_BASEURL");
        bool disableBaseUrl = !string.IsNullOrEmpty(disableBaseUrlEnv) && disableBaseUrlEnv == "1";

        HtmlLoadOptions loadOptions;
        if (disableBaseUrl)
        {
            loadOptions = new HtmlLoadOptions();
            Console.WriteLine("Base URL injection disabled.");
        }
        else
        {
            string basePath = Path.GetDirectoryName(Path.GetFullPath(htmlPath));
            loadOptions = new HtmlLoadOptions(basePath);
            Console.WriteLine("Base URL set to: " + basePath);
        }

        using (Document doc = new Document(htmlPath, loadOptions))
        {
            doc.Save(outputPdf);
        }

        Console.WriteLine("PDF saved to '" + outputPdf + "'.");
    }
}
