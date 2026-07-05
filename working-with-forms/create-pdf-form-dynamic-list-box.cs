using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "DynamicListForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle listRect = new Aspose.Pdf.Rectangle(100, 500, 300, 650);

            // Create a ListBox field attached to the document
            ListBoxField listBox = new ListBoxField(doc, listRect);
            listBox.Name = "DynamicList";
            listBox.PartialName = "DynamicList";
            listBox.MultiSelect = false; // single‑selection list box

            // Add initial items to the list
            listBox.AddOption("Item 1");
            listBox.AddOption("Item 2");
            listBox.AddOption("Item 3");

            // Add the field to page 1 of the document
            doc.Form.Add(listBox, 1);

            // Demonstrate adding more items later – the list expands automatically
            listBox.AddOption("Item 4 (added later)");
            listBox.AddOption("Item 5 (added later)");

            // Save the PDF with the form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with dynamic list saved to '{outputPath}'.");
    }
}