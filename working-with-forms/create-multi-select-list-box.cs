using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "listbox.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the list box (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, rect);

            // Enable multiselection
            listBox.MultiSelect = true;

            // Add options to the list box
            listBox.AddOption("Option A");
            listBox.AddOption("Option B");
            listBox.AddOption("Option C");
            listBox.AddOption("Option D");

            // Select multiple items (indices are 1‑based)
            listBox.SelectedItems = new int[] { 1, 3, 4 }; // selects "Option A", "Option C", "Option D"

            // Add the list box field to the document's form
            doc.Form.Add(listBox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multiselect list box saved to '{outputPath}'.");
    }
}