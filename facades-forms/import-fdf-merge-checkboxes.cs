using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string fdfFile = "checkboxes.fdf";
        const string outputPdf = "merged.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }
        if (!File.Exists(fdfFile))
        {
            Console.Error.WriteLine($"FDF file not found: {fdfFile}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            using (Form form = new Form(doc))
            {
                using (FileStream fdfStream = new FileStream(fdfFile, FileMode.Open, FileAccess.Read))
                {
                    form.ImportFdf(fdfStream);
                }
                // Save the modified document; existing fields are updated, no duplicates are created.
                doc.Save(outputPdf);
            }
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}