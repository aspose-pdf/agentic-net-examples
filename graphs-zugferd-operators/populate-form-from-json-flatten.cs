using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Step 1: create a sample PDF containing a text box form field
        using (Document sampleDoc = new Document())
        {
            Page page = sampleDoc.Pages.Add();
            // Rectangle(left, bottom, right, top)
            Rectangle fieldRect = new Rectangle(100, 600, 300, 650);
            TextBoxField nameField = new TextBoxField(page, fieldRect);
            nameField.PartialName = "Name";
            sampleDoc.Form.Add(nameField);
            sampleDoc.Save("sample.pdf");
        }

        // Step 2: write JSON data that matches the form field name
        string jsonContent = "{ \"Name\": \"John Doe\" }";
        File.WriteAllText("data.json", jsonContent);

        // Step 3: load the PDF, import values from JSON, flatten the form and save the result
        using (Document pdfDoc = new Document("sample.pdf"))
        {
            pdfDoc.Form.ImportFromJson("data.json");
            pdfDoc.Flatten();
            pdfDoc.Save("output.pdf");
        }
    }
}
