using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdf1Path = "first.pdf";
        const string pdf2Path = "second.pdf";
        const string outputPath = "merged.pdf";
        const string tempRenamedPdf2 = "second_renamed.pdf";

        if (!File.Exists(pdf1Path) || !File.Exists(pdf2Path))
        {
            Console.Error.WriteLine("One or both input files not found.");
            return;
        }

        // Load the second document, rename its form fields to avoid name collisions,
        // and save it to a temporary file.
        using (Document doc2 = new Document(pdf2Path))
        {
            foreach (Field field in doc2.Form.Fields)
            {
                field.Name = $"doc2_{field.Name}"; // make the name unique
            }
            doc2.Save(tempRenamedPdf2);
        }

        // Use PdfFileEditor.Concatenate to merge the first PDF with the renamed second PDF.
        // Concatenate preserves AcroForm fields.
        PdfFileEditor editor = new PdfFileEditor();
        editor.Concatenate(new string[] { pdf1Path, tempRenamedPdf2 }, outputPath);

        // Clean up the temporary file.
        if (File.Exists(tempRenamedPdf2))
        {
            File.Delete(tempRenamedPdf2);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPath}'.");
    }
}
