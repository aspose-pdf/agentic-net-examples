using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "multiselect_listbox.pdf";

        // Create a new PDF document and ensure proper disposal
        using (Document doc = new Document())
        {
            // Add a page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle for the list box (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, rect)
            {
                // Enable multiselection
                MultiSelect = true,
                // Optional: give the field a name
                Name = "Choices"
            };

            // Add options to the list box
            listBox.AddOption("Apple");
            listBox.AddOption("Banana");
            listBox.AddOption("Cherry");
            listBox.AddOption("Date");
            listBox.AddOption("Elderberry");

            // Select multiple items (indices are 1‑based)
            listBox.SelectedItems = new int[] { 2, 4, 5 }; // selects Banana, Date, Elderberry

            // Add the field to the document's form
            doc.Form.Add(listBox);

            // Save the PDF (no SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with multiselect list box saved to '{outputPath}'.");
    }
}