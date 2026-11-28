using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "RadioFilled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);

        // Retrieve the radio button field named "Gender"
        RadioButtonField genderField = pdfDocument.Form["Gender"] as RadioButtonField;
        if (genderField == null)
        {
            Console.Error.WriteLine("Radio button field 'Gender' not found.");
            return;
        }

        // The export value (or display name) of the option we want to select
        const string desiredExportValue = "Male";
        int selectedIndex = -1; // 1‑based index used by Aspose.Pdf

        // Iterate over the read‑only Options collection to find the matching option
        foreach (Option opt in genderField.Options)
        {
            // Option.Value holds the export value, Option.Name holds the display text
            if (opt.Value == desiredExportValue || opt.Name == desiredExportValue)
            {
                selectedIndex = opt.Index; // Index is already 1‑based
                break;
            }
        }

        if (selectedIndex > 0)
        {
            // Set the selected option using the 1‑based Selected property
            genderField.Selected = selectedIndex;
        }
        else
        {
            Console.Error.WriteLine($"Option '{desiredExportValue}' not found in the 'Gender' radio group.");
            return;
        }

        // Save the updated PDF
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Radio button filled and saved to '{outputPath}'.");
    }
}
