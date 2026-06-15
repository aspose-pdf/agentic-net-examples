using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "DynamicListForm.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle where the list box will appear
            // (lower‑left X, lower‑left Y, upper‑right X, upper‑right Y)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create a ListBoxField attached to the document
            ListBoxField listBox = new ListBoxField(doc, rect)
            {
                PartialName = "DynamicList",   // field name
                MultiSelect = true,            // allow multiple selections
                Color = Aspose.Pdf.Color.LightGray // background color
            };

            // Set a visible border (Border requires the parent annotation)
            listBox.Border = new Border(listBox) { Width = 1 };

            // Add some initial items to the list
            listBox.AddOption("Item 1");
            listBox.AddOption("Item 2");

            // Add the list box field to the form on page 1
            doc.Form.Add(listBox, 1);

            // Dynamically add more items – this could be called later at runtime
            AddItemToList(listBox, "Item 3");
            AddItemToList(listBox, "Item 4");

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with dynamic list field saved to '{outputPath}'.");
    }

    // Helper method to add a new option to the list box at runtime
    static void AddItemToList(ListBoxField listBox, string item)
    {
        listBox.AddOption(item);
    }
}