using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Create sample files that will be attached to the PDF
        string txtPath = "sample.txt";
        string pngPath = "sample.png";

        // Write a simple text file
        using (StreamWriter writer = new StreamWriter(txtPath))
        {
            writer.WriteLine("This is a sample text attachment.");
        }

        // Write a minimal PNG file (only the PNG signature bytes)
        byte[] pngSignature = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };
        using (FileStream fs = new FileStream(pngPath, FileMode.Create, FileAccess.Write))
        {
            fs.Write(pngSignature, 0, pngSignature.Length);
        }

        // -----------------------------------------------------------------
        // Step 1: Create a simple PDF document (self‑contained example)
        // -----------------------------------------------------------------
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save("input.pdf");
        }

        // -----------------------------------------------------------------
        // Step 2: Open the PDF and attach the files with custom MIME types
        // -----------------------------------------------------------------
        using (Document pdf = new Document("input.pdf"))
        {
            // Attach the text file
            FileSpecification txtSpec = new FileSpecification(txtPath);
            txtSpec.Description = "Sample text file attachment";
            txtSpec.MIMEType = "text/plain";
            pdf.EmbeddedFiles.Add(txtSpec);

            // Attach the PNG image file
            FileSpecification pngSpec = new FileSpecification(pngPath);
            pngSpec.Description = "Sample PNG image attachment";
            pngSpec.MIMEType = "image/png";
            pdf.EmbeddedFiles.Add(pngSpec);

            // Save the resulting PDF with the embedded files
            pdf.Save("output.pdf");
        }
    }
}
