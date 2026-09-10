using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;                         // Core PDF API
using Aspose.Pdf.Forms;                  // Form field classes
using Aspose.Pdf.Facades;                // FormEditor facade (if needed for other operations)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Retrieve the list (combo) box field named "CountryList"
            Aspose.Pdf.Forms.ListBoxField listField = doc.Form["CountryList"] as Aspose.Pdf.Forms.ListBoxField;

            if (listField == null)
            {
                Console.Error.WriteLine("Field 'CountryList' not found or is not a list box.");
                return;
            }

            // ----- Remove all existing items -----
            // Collect the names of all current options because we cannot modify the collection while iterating it.
            List<string> existingOptionNames = new List<string>();
            foreach (var option in listField.Options)
            {
                // Each option has a Name property that identifies it.
                existingOptionNames.Add(option.Name);
            }

            // Delete each option by its name.
            foreach (string optName in existingOptionNames)
            {
                listField.DeleteOption(optName);
            }

            // ----- Add new items -----
            string[] newCountries = new string[] { "United States", "Canada", "Mexico", "Germany", "France" };
            foreach (string country in newCountries)
            {
                listField.AddOption(country);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}