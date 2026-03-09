using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the existing PDF with FormEditor
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPdf);

            // ---------- Text Box (single line) ----------
            formEditor.AddField(FieldType.Text, "TextBox1", 1, 100, 700, 250, 720);
            // Set multiline flag for a separate text box
            formEditor.AddField(FieldType.Text, "MultiLineTextBox", 1, 100, 650, 250, 690);
            var multiLineField = formEditor.Document.Form["MultiLineTextBox"] as TextBoxField;
            if (multiLineField != null)
                multiLineField.Multiline = true;

            // ---------- Check Box ----------
            formEditor.AddField(FieldType.CheckBox, "CheckBox1", 1, 100, 600, 115, 615);

            // ---------- Radio Button Group ----------
            formEditor.AddField(FieldType.Radio, "RadioGroup1", 1, 100, 540, 200, 560);
            var radioField = formEditor.Document.Form["RadioGroup1"] as RadioButtonField;
            if (radioField != null)
            {
                radioField.AddOption("OptionA");
                radioField.AddOption("OptionB");
                radioField.AddOption("OptionC");
            }

            // ---------- List Box ----------
            formEditor.AddField(FieldType.ListBox, "ListBox1", 1, 300, 700, 400, 800);
            formEditor.AddListItem("ListBox1", "Item 1");
            formEditor.AddListItem("ListBox1", "Item 2");
            formEditor.AddListItem("ListBox1", "Item 3");

            // ---------- Combo Box ----------
            formEditor.AddField(FieldType.ComboBox, "ComboBox1", 1, 300, 600, 400, 620);
            // Add items with export values (label, export value)
            formEditor.AddListItem("ComboBox1", new string[] { "Display1", "Value1" });
            formEditor.AddListItem("ComboBox1", new string[] { "Display2", "Value2" });
            formEditor.AddListItem("ComboBox1", new string[] { "Display3", "Value3" });

            // Save the updated PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"Form fields added successfully. Output saved to '{outputPdf}'.");
    }
}