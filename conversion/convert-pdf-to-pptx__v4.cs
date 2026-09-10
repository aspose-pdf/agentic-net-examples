using System;
using System.IO;
using Aspose.Pdf; // SaveFormat lives in the root Aspose.Pdf namespace

class Program
{
    static void Main()
    {
        // Paths
        const string pdfPath = "input.pdf";
        const string outputPptxPath = "output.pptx";
        const string templatePath = "template.pptx"; // kept for validation only

        // Validate input PDF
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Optional: validate that a template file exists. The template cannot be applied
        // because Aspose.Slides is not referenced in this project.
        if (!File.Exists(templatePath))
        {
            Console.Error.WriteLine($"Slide master template not found (will be ignored): {templatePath}");
        }

        // ---------- Convert PDF to PPTX using Aspose.Pdf only ----------
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Directly save the PDF as a PPTX file. No additional SaveOptions are required.
            pdfDoc.Save(outputPptxPath, SaveFormat.Pptx);
        }

        Console.WriteLine($"Conversion complete. Output saved to '{outputPptxPath}'.");
    }
}
