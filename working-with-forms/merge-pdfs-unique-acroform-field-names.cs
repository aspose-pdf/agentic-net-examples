using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string firstPdf = "first.pdf";
        const string secondPdf = "second.pdf";
        const string outputPdf = "merged.pdf";

        if (!File.Exists(firstPdf) || !File.Exists(secondPdf))
        {
            Console.Error.WriteLine("One or both input files are missing.");
            return;
        }

        // Load documents
        using (Document target = new Document(firstPdf))
        using (Document source = new Document(secondPdf))
        {
            // Prefix all field names in the source document to avoid name clashes
            string prefix = Path.GetFileNameWithoutExtension(secondPdf) + "_";

            foreach (var field in source.Form.Fields)
            {
                field.Name = prefix + field.Name;
            }

            // Append source pages to target
            target.Pages.Add(source.Pages);

            // Copy renamed fields into target form
            foreach (var field in source.Form.Fields)
            {
                // The Form.Add method clones the field into the target document.
                target.Form.Add(field);
            }

            // Save merged document
            target.Save(outputPdf);
        }

        Console.WriteLine($"Merged PDF saved to '{outputPdf}'.");
    }
}
