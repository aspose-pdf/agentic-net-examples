using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "CountryList";

        // New items to populate the dropdown
        string[] newItems = { "USA", "Canada", "Mexico", "Germany", "France" };

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // The Form indexer returns a WidgetAnnotation; cast it to a Field first
            Field field = doc.Form[fieldName] as Field;
            if (field == null)
            {
                Console.Error.WriteLine($"Field '{fieldName}' not found or is not a form field.");
                return;
            }

            // Cast to ChoiceField to manipulate dropdown/list options
            if (field is ChoiceField choiceField)
            {
                // Remove all existing options
                while (choiceField.Options.Count > 0)
                {
                    // Options are indexed; delete the first one repeatedly
                    string optionName = choiceField.Options[0].Name;
                    choiceField.DeleteOption(optionName);
                }

                // Add new items
                foreach (string item in newItems)
                {
                    choiceField.AddOption(item);
                }
            }
            else
            {
                Console.Error.WriteLine($"Field '{fieldName}' is not a choice field.");
                return;
            }

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Dropdown field '{fieldName}' cleared and repopulated. Saved to '{outputPdf}'.");
    }
}
