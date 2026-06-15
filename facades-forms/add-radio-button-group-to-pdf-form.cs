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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Initialize FormEditor with the loaded document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Name of the radio button group
                string fieldName = "PaymentMethod";

                // Define the options for the group
                formEditor.Items = new string[] { "Credit", "PayPal" };

                // Arrange the radio buttons vertically (false = vertical, true = horizontal)
                formEditor.RadioHoriz = false;

                // Add the radio button field on page 3.
                // Rectangle coordinates: lower‑left (100, 500), upper‑right (200, 540)
                bool added = formEditor.AddField(FieldType.Radio, fieldName, 3, 100, 500, 200, 540);
                if (!added)
                {
                    Console.Error.WriteLine("Failed to add the radio button field.");
                }

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"Radio button group \"{outputPath}\" created successfully.");
    }
}