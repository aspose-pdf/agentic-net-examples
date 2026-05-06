using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure a PDF exists to edit; create a blank one if missing
        if (!File.Exists(inputPath))
        {
            using (Document doc = new Document())
            {
                doc.Pages.Add(); // add a single blank page
                doc.Save(inputPath);
            }
        }

        // Load the PDF, add a list box field, and save the result
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Define the items for the list box
            formEditor.Items = new string[] { "Low", "Medium", "High" };

            // Add the list box field named "Priority" on page 1
            // Parameters: field type, field name, default value, page number,
            // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            formEditor.AddField(FieldType.ListBox, "Priority", "Medium", 1, 100, 500, 200, 550);

            // Persist changes to the document
            doc.Save(outputPath);
        }

        Console.WriteLine($"List field 'Priority' added and saved to '{outputPath}'.");
    }
}