using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Initialize FormEditor on the document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a ListBox field that will hold the dynamic items
                // Parameters: field type, field name, page number, llx, lly, urx, ury
                formEditor.AddField(FieldType.ListBox, "DynamicList", 1, 100, 600, 250, 650);

                // Add a PushButton that the user will click to add a new item
                formEditor.AddField(FieldType.PushButton, "AddItemBtn", 1, 260, 600, 360, 630);

                // JavaScript that adds a new entry to the ListBox each time the button is pressed
                string js = @"
                    var list = this.getField('DynamicList');
                    var cnt = list.numItems;
                    // Insert a new item at the end of the list
                    list.insertItem('Item ' + (cnt + 1), -1);
                ";

                // Attach the JavaScript to the button's mouse‑up action
                formEditor.AddFieldScript("AddItemBtn", js);

                // Save the PDF with the interactive form
                formEditor.Save("DynamicListForm.pdf");
            }
        }

        Console.WriteLine("PDF form with dynamic list field created: DynamicListForm.pdf");
    }
}