using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing a form
        const string outputPdf = "output.pdf";  // PDF with the new combo box

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a FormEditor bound to the loaded document
            FormEditor formEditor = new FormEditor(doc);

            // Add a combo box field named "State" on page 1.
            // Rectangle coordinates: lower‑left (100, 500), upper‑right (200, 520)
            formEditor.AddField(FieldType.ComboBox, "State", 1, 100f, 500f, 200f, 520f);

            // US state abbreviations to populate the combo box
            string[] stateAbbreviations = new string[]
            {
                "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
                "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
                "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
                "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
                "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
            };

            // Add each abbreviation as an item in the combo box
            foreach (string abbrev in stateAbbreviations)
            {
                formEditor.AddListItem("State", abbrev);
            }

            // Save the modified PDF to the output file
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Combo box \"State\" added and saved to '{outputPdf}'.");
    }
}