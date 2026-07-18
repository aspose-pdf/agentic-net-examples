using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";   // existing PDF with a form
        const string outputPdf = "output.pdf";

        // Verify the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor handles opening the source PDF and writing the result
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a ComboBox field named "State" on page 1.
            // Rectangle coordinates: lower‑left (llx,lly) and upper‑right (urx,ury)
            formEditor.AddField(FieldType.ComboBox, "State", 1, 100, 500, 200, 530);

            // US state abbreviations to populate the combo box
            string[] states = new string[]
            {
                "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
                "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
                "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
                "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
                "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
            };

            // Add each abbreviation as an item in the combo box
            foreach (string state in states)
            {
                formEditor.AddListItem("State", state);
            }

            // Persist the changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Combo box \"State\" added and saved to '{outputPdf}'.");
    }
}