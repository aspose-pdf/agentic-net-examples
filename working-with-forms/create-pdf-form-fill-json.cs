using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

public class Program
{
    public static void Main()
    {
        // Step 1: Create a PDF template with form fields
        using (Document templateDoc = new Document())
        {
            Page page = templateDoc.Pages.Add();

            // Name field
            Rectangle nameRect = new Rectangle(100f, 600f, 300f, 620f);
            TextBoxField nameField = new TextBoxField(page, nameRect);
            nameField.PartialName = "Name";
            nameField.Value = "Enter name";
            templateDoc.Form.Add(nameField);

            // Email field
            Rectangle emailRect = new Rectangle(100f, 560f, 300f, 580f);
            TextBoxField emailField = new TextBoxField(page, emailRect);
            emailField.PartialName = "Email";
            emailField.Value = "Enter email";
            templateDoc.Form.Add(emailField);

            templateDoc.Save("template.pdf");
        }

        // Step 2: Create a JSON file with data for the form fields
        string jsonContent = @"{
  ""formFields"": [
    { ""partialName"": ""Name"", ""value"": ""John Doe"" },
    { ""partialName"": ""Email"", ""value"": ""john.doe@example.com"" }
  ]
}";
        using (FileStream jsonStream = new FileStream("data.json", FileMode.Create, FileAccess.Write))
        using (StreamWriter writer = new StreamWriter(jsonStream))
        {
            writer.Write(jsonContent);
        }

        // Step 3: Load the template and import JSON data into the form
        using (Document filledDoc = new Document("template.pdf"))
        {
            using (FileStream jsonStream = new FileStream("data.json", FileMode.Open, FileAccess.Read))
            {
                filledDoc.Form.ImportFromJson(jsonStream);
            }
            filledDoc.Save("filled.pdf");
        }
    }
}
