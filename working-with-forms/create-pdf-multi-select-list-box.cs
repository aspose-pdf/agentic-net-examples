using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page to host the list box
            Page page = doc.Pages.Add();

            // Define the rectangle for the list box (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 650);

            // Create the ListBoxField on the page
            ListBoxField listBox = new ListBoxField(page, rect)
            {
                // Set a name for the field (used to retrieve its value later)
                Name = "MultiSelectList",
                // Enable multiple selection
                MultiSelect = true
            };

            // Add options to the list box
            listBox.AddOption("Option A");
            listBox.AddOption("Option B");
            listBox.AddOption("Option C");
            listBox.AddOption("Option D");

            // Add the list box to the document's form
            doc.Form.Add(listBox);

            // Save the PDF with the form
            doc.Save("MultiSelectListBox.pdf");
        }

        Console.WriteLine("PDF with multi‑select list box created successfully.");
    }
}