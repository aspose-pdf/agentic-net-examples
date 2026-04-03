using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the existing PDF
        using (Document doc = new Document(inputPdf))
        {
            // Create a rectangle for the list box (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 250, 650);

            // Create the ListBox field on the first page
            ListBoxField listBox = new ListBoxField(doc.Pages[1], rect);

            // Enable multi‑selection
            listBox.MultiSelect = true;

            // Add options to the list box
            listBox.AddOption("Option 1");
            listBox.AddOption("Option 2");
            listBox.AddOption("Option 3");
            listBox.AddOption("Option 4");
            listBox.AddOption("Option 5");

            // Pre‑select three items (indices are 1‑based)
            listBox.SelectedItems = new int[] { 1, 2, 3 };

            // Add the field to the page annotations collection
            doc.Pages[1].Annotations.Add(listBox);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}