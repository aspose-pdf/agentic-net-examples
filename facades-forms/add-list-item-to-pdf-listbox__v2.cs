using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "survey_form_v2.pdf";
        const string outputPath = "updated_feedback.pdf";
        const string listBoxName = "Feedback";
        const string newItem     = "Very Satisfied";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a FormEditor, bind the existing PDF, modify the form, and save.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF into the editor.
            formEditor.BindPdf(inputPath);

            // Add a new list item to the ListBox field named "Feedback".
            // FormEditor provides AddListItem (not AddListBoxItem) for this purpose.
            formEditor.AddListItem(listBoxName, newItem);

            // Save the updated PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
