using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // US state abbreviations to be added to the combo box
        string[] usStates = new string[]
        {
            "AL","AK","AZ","AR","CA","CO","CT","DE","FL","GA",
            "HI","ID","IL","IN","IA","KS","KY","LA","ME","MD",
            "MA","MI","MN","MS","MO","MT","NE","NV","NH","NJ",
            "NM","NY","NC","ND","OH","OK","OR","PA","RI","SC",
            "SD","TN","TX","UT","VT","VA","WA","WV","WI","WY"
        };

        // ---------------------------------------------------------------------
        // Create a minimal PDF in memory so we don't depend on an external file.
        // This avoids the FileNotFoundException that occurred when "input.pdf"
        // was missing. The PDF is written to a MemoryStream, which is then bound
        // to the FormEditor via the Stream overload of BindPdf.
        // ---------------------------------------------------------------------
        using (MemoryStream inputStream = new MemoryStream())
        {
            // Create a blank PDF with a single page.
            Document blankDoc = new Document();
            blankDoc.Pages.Add();
            blankDoc.Save(inputStream);
            // Reset the stream position before reading it again.
            inputStream.Position = 0;

            // FormEditor is a facade for editing AcroForm fields.
            // It implements IDisposable, so we wrap it in a using block.
            using (FormEditor formEditor = new FormEditor())
            {
                // Bind the in‑memory PDF.
                formEditor.BindPdf(inputStream);

                // Add a ComboBox field named "State" on page 1.
                // Rectangle coordinates: lower‑left (llx,lly) and upper‑right (urx,ury).
                // Adjust these values as needed for your layout.
                formEditor.AddField(FieldType.ComboBox, "State", 1, 100f, 500f, 200f, 520f);

                // Populate the combo box with the state abbreviations using AddListItem.
                foreach (string state in usStates)
                {
                    formEditor.AddListItem("State", state);
                }

                // Save the modified PDF to the output file.
                formEditor.Save("output.pdf");
            }
        }

        Console.WriteLine("Combo box 'State' added and populated. Output saved to 'output.pdf'.");
    }
}
