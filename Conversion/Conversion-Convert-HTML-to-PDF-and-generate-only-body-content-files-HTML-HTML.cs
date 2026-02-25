using System;
using System.IO;
using Aspose.Pdf;          // All SaveOptions subclasses are in this namespace

class Program
{
    static void Main()
    {
        const string htmlInputPath      = "input.html";
        const string pdfOutputPath      = "output.pdf";
        const string bodyOnlyHtmlPath   = "body_only.html";

        if (!File.Exists(htmlInputPath))
        {
            Console.Error.WriteLine($"Input file not found: {htmlInputPath}");
            return;
        }

        // Load the HTML file. HtmlLoadOptions can be omitted if defaults are sufficient.
        HtmlLoadOptions loadOptions = new HtmlLoadOptions();

        // Wrap Document in a using block for deterministic disposal.
        using (Document doc = new Document(htmlInputPath, loadOptions))
        {
            // -----------------------------------------------------------------
            // 1) Convert HTML → PDF (standard PDF save, no SaveOptions needed)
            // -----------------------------------------------------------------
            doc.Save(pdfOutputPath);   // Saves as PDF because no SaveOptions are supplied

            // -----------------------------------------------------------------
            // 2) Generate HTML that contains ONLY the <body> markup.
            //    HtmlMarkupGenerationMode = WriteOnlyBodyContent strips everything
            //    outside the <body> element.
            // -----------------------------------------------------------------
            HtmlSaveOptions htmlOpts = new HtmlSaveOptions
            {
                HtmlMarkupGenerationMode = HtmlSaveOptions.HtmlMarkupGenerationModes.WriteOnlyBodyContent
            };

            // Must pass HtmlSaveOptions explicitly; otherwise a PDF would be written.
            doc.Save(bodyOnlyHtmlPath, htmlOpts);
        }

        Console.WriteLine($"Conversion completed:");
        Console.WriteLine($"  PDF output      : {pdfOutputPath}");
        Console.WriteLine($"  Body‑only HTML  : {bodyOnlyHtmlPath}");
    }
}