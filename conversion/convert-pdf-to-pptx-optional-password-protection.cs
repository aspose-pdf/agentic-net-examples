using System;
using System.IO;
using Aspose.Pdf; // Core PDF API

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string pptxPath = "output.pptx";
        const string pptxPassword = "Secure123"; // retained for reference

        // -----------------------------------------------------------------
        // 1. Convert PDF to PPTX using Aspose.Pdf (rule‑based conversion)
        // -----------------------------------------------------------------
        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF file not found: {pdfPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document pdfDoc = new Document(pdfPath))
        {
            // Directly save as PPTX – Aspose.Pdf supports this format natively.
            pdfDoc.Save(pptxPath, SaveFormat.Pptx);
        }

        // -----------------------------------------------------------------
        // 2. (Optional) Protect the generated PPTX with a password.
        // -----------------------------------------------------------------
        // Password protection of PPTX files is provided by Aspose.Slides.
        // If the Aspose.Slides assembly is referenced, the following code can be used:
        //
        // using Aspose.Slides;
        // using Aspose.Slides.Export;
        //
        // using (Presentation pres = new Presentation(pptxPath))
        // {
        //     pres.ProtectionManager.SetPassword(pptxPassword);
        //     pres.Save(pptxPath, SaveFormat.Pptx);
        // }
        //
        // In environments where Aspose.Slides is not available, the PPTX will remain
        // unprotected.

        if (!File.Exists(pptxPath))
        {
            Console.Error.WriteLine($"PPTX file not created: {pptxPath}");
            return;
        }

        Console.WriteLine($"PDF successfully converted to PPTX: {pptxPath}");
    }
}
