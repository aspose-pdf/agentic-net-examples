using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "StateComboBox.pdf";

        // Create a new PDF document and add a blank page.
        using (Document doc = new Document())
        {
            doc.Pages.Add(); // ensure at least one page

            // Define the rectangle where the combo box will appear (llx, lly, urx, ury).
            // Use the fully‑qualified Aspose.Pdf.Rectangle to avoid ambiguity with System.Drawing.Rectangle.
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 600, 200, 620);

            // Create the combo box field.
            ComboBoxField combo = new ComboBoxField(doc.Pages[1], comboRect)
            {
                PartialName = "State",
                Value = string.Empty // optional default value
            };

            // Add the field to the document's form (page index is 1‑based).
            doc.Form.Add(combo, 1);

            // List of US state abbreviations.
            string[] states = new string[]
            {
                "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
                "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
                "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
                "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
                "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
            };

            // Populate the combo box with each state abbreviation.
            foreach (string state in states)
            {
                // ComboBoxField uses the Options collection. AddOption(value, displayText) adds an entry.
                combo.AddOption(state, state);
            }

            // Save the resulting PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with State combo box saved to '{outputPath}'.");
    }
}
