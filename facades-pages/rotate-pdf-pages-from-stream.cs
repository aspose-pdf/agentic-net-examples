using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Simulate obtaining a PDF from a network stream.
        // If a local file "input.pdf" exists we use it, otherwise we create a simple PDF in memory.
        using (MemoryStream pdfStream = new MemoryStream())
        {
            if (File.Exists("input.pdf"))
            {
                byte[] pdfBytes = File.ReadAllBytes("input.pdf");
                pdfStream.Write(pdfBytes, 0, pdfBytes.Length);
                pdfStream.Position = 0;
            }
            else
            {
                // Create a minimal one‑page PDF on the fly.
                using (Document doc = new Document())
                {
                    doc.Pages.Add();
                    doc.Save(pdfStream);
                    pdfStream.Position = 0;
                }
            }

            // Rotate all pages by 90 degrees.
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(pdfStream);
                editor.Rotation = 90; // allowed values: 0, 90, 180, 270
                editor.ApplyChanges();

                // Overwrite the original stream with the rotated content.
                pdfStream.Position = 0;
                editor.Save(pdfStream);
            }

            // The stream now contains the rotated PDF – write it out or send it.
            File.WriteAllBytes("rotated_output.pdf", pdfStream.ToArray());
        }
    }
}