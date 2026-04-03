using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF document (using statement ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the form fields collection
            Form form = doc.Form;

            // Retrieve the 'Country' combo box field
            ComboBoxField countryField = form["Country"] as ComboBoxField;

            if (countryField != null)
            {
                // Iterate over the available options to find "United States"
                foreach (Option opt in countryField.Options)
                {
                    if (opt.Name == "United States")
                    {
                        // Set the selected index to the option's index
                        countryField.Selected = opt.Index;
                        break;
                    }
                }
            }
            else
            {
                Console.Error.WriteLine("ComboBox field 'Country' not found.");
            }

            // Save the modified PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Saved updated PDF to '{outputPath}'.");
    }
}