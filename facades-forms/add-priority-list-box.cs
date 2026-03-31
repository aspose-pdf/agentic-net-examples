using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "priority_list.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the list box (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100.0, 500.0, 250.0, 650.0);

            // Create the ListBox field on the page
            ListBoxField listBox = new ListBoxField(page, rect);
            listBox.Name = "Priority";
            listBox.PartialName = "Priority";

            // Add items to the list box
            listBox.AddOption("Low");
            listBox.AddOption("Medium");
            listBox.AddOption("High");

            // Set default selected item to "Medium" (items are 1‑based)
            listBox.Selected = 2;

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine("PDF with Priority list field created.");
    }
}