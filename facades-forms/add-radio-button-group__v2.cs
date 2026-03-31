using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string tempInput = "input.pdf";
        const string output = "output.pdf";

        // Create a blank PDF with one page
        using (Document doc = new Document())
        {
            doc.Pages.Add();
            doc.Save(tempInput);
        }

        // Add a radio button group named "Gender" with three options
        FormEditor formEditor = new FormEditor(tempInput, output);
        formEditor.RadioHoriz = true; // arrange horizontally
        formEditor.RadioGap = 20; // gap between buttons
        formEditor.Items = new string[] { "Male", "Female", "Other" };
        // AddField(FieldType.Radio, fieldName, defaultValue, pageNumber, llx, lly, urx, ury)
        formEditor.AddField(FieldType.Radio, "Gender", "Male", 1, 100, 700, 200, 720);
        formEditor.Save();

        // Clean up temporary file
        File.Delete(tempInput);
        Console.WriteLine("Radio button group added to " + output);
    }
}