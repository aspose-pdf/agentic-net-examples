using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        // Paths and password for the encrypted PDF
        const string encryptedPdfPath = "encrypted_input.pdf";
        const string userPassword     = "user123";
        const string intermediatePath = "aligned.pdf";
        const string finalOutputPath  = "final_output.pdf";

        if (!File.Exists(encryptedPdfPath))
        {
            Console.Error.WriteLine($"File not found: {encryptedPdfPath}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the encrypted PDF with a password using Document,
        //    then bind that Document to PdfPageEditor for page‑level
        //    manipulations.
        // ------------------------------------------------------------
        Document encryptedDoc = new Document(encryptedPdfPath, userPassword);
        PdfPageEditor editor = new PdfPageEditor();
        // BindPdf has an overload that accepts a Document instance.
        editor.BindPdf(encryptedDoc);
        editor.ProcessPages = new int[] { 1 }; // 1‑based page numbers
        editor.HorizontalAlignment = HorizontalAlignment.Center; // example alignment
        editor.Save(intermediatePath); // save intermediate result (still encrypted)

        // ------------------------------------------------------------
        // 2. Load the intermediate PDF (still encrypted) with the same
        //    password and perform structural page manipulations using the
        //    Document API.
        // ------------------------------------------------------------
        using (Document doc = new Document(intermediatePath, userPassword))
        {
            // Delete the second page (if it exists)
            if (doc.Pages.Count >= 2)
            {
                doc.Pages.Delete(2); // Pages are 1‑based
            }

            // Optionally add a text annotation to the first page
            if (doc.Pages.Count >= 1)
            {
                Page firstPage = doc.Pages[1];
                Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);
                TextAnnotation annotation = new TextAnnotation(firstPage, rect)
                {
                    Title    = "Note",
                    Contents = "Page processed with password.",
                    Open     = true,
                    Icon     = TextIcon.Note
                };
                firstPage.Annotations.Add(annotation);
            }

            // Save the final PDF (still encrypted with the original password)
            doc.Save(finalOutputPath);
        }

        // Clean up the intermediate file
        try { File.Delete(intermediatePath); } catch { /* ignore */ }

        Console.WriteLine($"Processing completed. Output saved to '{finalOutputPath}'.");
    }
}
