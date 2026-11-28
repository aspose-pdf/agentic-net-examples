using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "OptionsForm.pdf";
        const string outputPath = "ColorChoices.txt";
        const string fieldName = "ColorChoice";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document.
        using (Document doc = new Document(pdfPath))
        {
            // Find the choice field (ComboBox or ListBox) with the given name.
            var choiceField = doc.Form?.Fields?
                .FirstOrDefault(f => string.Equals(f.FullName, fieldName, StringComparison.OrdinalIgnoreCase) ||
                                      string.Equals(f.Name, fieldName, StringComparison.OrdinalIgnoreCase)) as ChoiceField;

            if (choiceField == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a choice field.");
                return;
            }

            // Convert the OptionCollection to a string array safely.
            string[] options = choiceField.Options != null
                ? choiceField.Options.Cast<string>().ToArray()
                : Array.Empty<string>();

            // Write the options to a text file, one per line.
            File.WriteAllLines(outputPath, options);
        }

        Console.WriteLine($"Options for field '{fieldName}' have been written to '{outputPath}'.");
    }
}
