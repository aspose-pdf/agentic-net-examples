using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Rectangle for the ListBox field
            Aspose.Pdf.Rectangle listRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            ListBoxField listBox = new ListBoxField(page, listRect)
            {
                PartialName = "myList",
                Color = Aspose.Pdf.Color.LightGray
            };
            // Border must be set after the object is instantiated
            listBox.Border = new Border(listBox) { Width = 1 };

            // Initial items
            listBox.AddOption("Item 1");
            listBox.AddOption("Item 2");

            doc.Form.Add(listBox);

            // Rectangle for the button that adds items
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(320, 500, 420, 540);
            ButtonField addButton = new ButtonField(page, btnRect)
            {
                PartialName = "addBtn",
                Contents = "Add Item",
                Color = Aspose.Pdf.Color.LightBlue
            };
            addButton.Border = new Border(addButton) { Width = 1 };

            // JavaScript that inserts a new item into the ListBox
            string jsCode = @"
                var f = this.getField('myList');
                var now = new Date();
                var item = 'Item ' + now.getTime();
                f.insertItem(item, item);
                f.currentValue = item;
            ";

            // Attach the script to the button's mouse‑up action (click)
            addButton.Actions.OnReleaseMouseBtn = new JavascriptAction(jsCode);

            doc.Form.Add(addButton);

            doc.Save("dynamic_list_form.pdf");
        }

        Console.WriteLine("PDF with dynamic ListBox created: dynamic_list_form.pdf");
    }
}
