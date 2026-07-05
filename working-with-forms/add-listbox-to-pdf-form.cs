using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "listbox_form.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the list box (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 250, 650);

            // Create the list box field on the page
            ListBoxField listBox = new ListBoxField(page, rect);
            listBox.Name = "CountryList";
            listBox.PartialName = "CountryList";
            // Optional visual styling
            listBox.Color = Aspose.Pdf.Color.LightGray;
            // Border class is not available in the current Aspose.Pdf version; omit or use alternative styling if needed.
            // listBox.Border = new Border(listBox) { Width = 1 };

            // Add country options to the list box
            listBox.AddOption("United States");
            listBox.AddOption("Canada");
            listBox.AddOption("United Kingdom");
            listBox.AddOption("Australia");
            listBox.AddOption("Germany");
            listBox.AddOption("France");
            listBox.AddOption("Japan");
            listBox.AddOption("China");
            listBox.AddOption("India");
            listBox.AddOption("Brazil");

            // Add the list box field to the document's form
            doc.Form.Add(listBox);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with list box saved to '{outputPath}'.");
    }
}
