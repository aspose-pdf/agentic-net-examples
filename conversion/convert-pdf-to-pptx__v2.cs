using System;
using System.IO;
using Aspose.Pdf;               // PDF handling and conversion
using Aspose.Pdf.Drawing;        // Required for ImageStamp etc. (if needed)

class Program
{
    static void Main()
    {
        // Input PDF, intermediate PPTX, and desired password (not used – PPTX password protection requires Aspose.Slides which is not referenced)
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string pptxPassword = "Secure123"; // retained for API compatibility, but not applied

        // Verify the source PDF exists
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"Source PDF not found: {pdfPath}");
            return;
        }

        // ---------- Convert PDF → PPTX ----------
        // Load the PDF document
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Aspose.Pdf can directly save a PDF as PPTX without needing Aspose.Slides.
            // The format is inferred from the SaveFormat enum.
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // ---------- (Optional) Protect the generated PPTX ----------
        // Password‑protecting a PPTX file is a feature of Aspose.Slides (ProtectionManager).
        // Since the current project does not reference Aspose.Slides, this step is omitted.
        // If Aspose.Slides is added later, the following pattern can be used:
        //
        // using (var pres = new Aspose.Slides.Presentation(pptxPath))
        // {
        //     pres.ProtectionManager.EncryptDocument(pptxPassword);
        //     pres.Save(pptxPath, Aspose.Slides.Export.SaveFormat.Pptx);
        // }

        Console.WriteLine($"PDF converted to PPTX successfully: {pptxPath}");
    }
}
