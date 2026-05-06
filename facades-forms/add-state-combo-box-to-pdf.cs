using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "ComboBoxState.pdf";

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Initialize FormEditor with the document instance
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a combo box field named "State" on page 1
                // Rectangle defined by lower‑left (100, 500) and upper‑right (200, 520)
                formEditor.AddField(FieldType.ComboBox, "State", 1, 100, 500, 200, 520);

                // US state abbreviations
                string[] states = new string[]
                {
                    "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
                    "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
                    "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
                    "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
                    "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
                };

                // Populate the combo box using AddListItem
                foreach (string state in states)
                {
                    formEditor.AddListItem("State", state);
                }

                // Save the changes to the bound document (specify destination as required by the API)
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with combo box saved to '{outputPath}'.");
    }
}
