using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "application.pdf";
        const string outputPath = "result.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the PDF to a FormEditor instance and add a required text field on page 3
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Ensure the document has at least three pages
            if (formEditor.Document.Pages.Count < 3)
            {
                Console.Error.WriteLine("The PDF does not contain a third page.");
                return;
            }

            // Add a text field named "ApplicantName" on page 3
            // Parameters: FieldType, name, pageNumber, llx, lly, urx, ury
            formEditor.AddField(FieldType.Text, "ApplicantName", 3, 100, 500, 300, 520);

            // Mark the field as required
            formEditor.SetFieldAttribute("ApplicantName", PropertyFlag.Required);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Result saved to '{outputPath}'.");
    }
}
