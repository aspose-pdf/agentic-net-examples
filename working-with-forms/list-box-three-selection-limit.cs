using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input and output PDF paths
        const string inputPdf  = "input.pdf";   // existing PDF (can be empty)
        const string outputPdf = "output.pdf";

        // Ensure the input PDF exists; if not, create a blank one
        if (!File.Exists(inputPdf))
        {
            using (var doc = new Document())
            {
                doc.Pages.Add();
                doc.Save(inputPdf);
            }
        }

        // Open the document
        using (var doc = new Document(inputPdf))
        {
            // Use the first page (1‑based indexing)
            Page page = doc.Pages[1];

            // Define the rectangle where the list box will appear
            // (llx, lly, urx, ury) – coordinates are in points
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 250, 700);

            // Create a ListBox field
            ListBoxField listBox = new ListBoxField(page, rect)
            {
                // Enable multi‑selection
                MultiSelect = true,

                // Give the field a name (used for later reference)
                PartialName = "MyListBox"
            };

            // Add options to the list box
            listBox.AddOption("Option 1");
            listBox.AddOption("Option 2");
            listBox.AddOption("Option 3");
            listBox.AddOption("Option 4");
            listBox.AddOption("Option 5");

            // Set the maximum number of selections by pre‑selecting three items.
            // The SelectedItems property expects an array of 1‑based indices.
            // Selecting three items demonstrates the intended limit.
            listBox.SelectedItems = new int[] { 1, 2, 3 };

            // Add the list box to the document's form collection
            doc.Form.Add(listBox);

            // Save the updated PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"List box with a three‑item selection limit saved to '{outputPdf}'.");
    }
}