using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Bind a FormEditor to the document
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(doc);

                // Copy the inner field "FirstName" to a new field "FirstNameCopy" on page 1
                // FormEditor provides CopyInnerField for this purpose.
                editor.CopyInnerField("FirstName", "FirstNameCopy", 1);

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Output saved to '{outputPath}'.");
    }
}
