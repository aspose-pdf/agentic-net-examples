using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF, set metadata, then convert to PPTX.
        using (Document pdfDoc = new Document(pdfPath))
        {
            // ---- Standard PDF properties ----
            pdfDoc.Info.Subject = "Quarterly Report"; // existing property

            // ---- Custom metadata (Company, Project, etc.) ----
            // DocumentInfo exposes an indexer for arbitrary key/value pairs.
            pdfDoc.Info["Company"] = "Acme Corp";
            pdfDoc.Info["Project"] = "Apollo";

            // Convert the PDF to PPTX.
            var pptxOptions = new PptxSaveOptions();
            pdfDoc.Save(pptxPath, pptxOptions);
        }

        Console.WriteLine($"PDF converted to PPTX with properties saved at '{pptxPath}'.");
    }
}
