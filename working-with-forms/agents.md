---
name: working-with-forms
description: C# examples for working-with-forms using Aspose.PDF for .NET
language: csharp
framework: net10.0
parent: ../agents.md
---

# AGENTS - working-with-forms

## Persona

You are a C# developer specializing in PDF processing using Aspose.PDF for .NET,
working within the **working-with-forms** category.
This folder contains standalone C# examples for working-with-forms operations.
See the root [agents.md](../agents.md) for repository-wide conventions and boundaries.

## Scope
- This folder contains examples for **working-with-forms**.
- Files are standalone `.cs` examples stored directly in this folder.

## Required Namespaces

- `using Aspose.Pdf;` (421/450 files) ← category-specific
- `using Aspose.Pdf.Forms;` (292/450 files) ← category-specific
- `using Aspose.Pdf.Annotations;` (108/450 files)
- `using Aspose.Pdf.Facades;` (97/450 files)
- `using Aspose.Pdf.Text;` (28/450 files)
- `using Aspose.Pdf.Drawing;` (24/450 files)
- `using Aspose.Pdf.Security;` (1/450 files)
- `using Aspose.Pdf.Tagged;` (1/450 files)
- `using System;` (450/450 files)
- `using System.IO;` (412/450 files)
- `using System.Collections.Generic;` (30/450 files)
- `using System.Xml;` (27/450 files)
- `using System.Text;` (12/450 files)
- `using System.Drawing;` (9/450 files)
- `using System.Threading.Tasks;` (9/450 files)
- `using System.Linq;` (7/450 files)
- `using System.Net.Http;` (7/450 files)
- `using System.Xml.Linq;` (6/450 files)
- `using System.Text.Json;` (5/450 files)
- `using System.Data;` (4/450 files)
- `using System.IO.Compression;` (4/450 files)
- `using System.Xml.Xsl;` (3/450 files)
- `using System.Runtime.InteropServices;` (2/450 files)
- `using System.Security.Cryptography;` (2/450 files)
- `using System.Text.RegularExpressions;` (2/450 files)
- `using System.Threading;` (2/450 files)
- `using System.Xml.Schema;` (2/450 files)
- `using System.Drawing.Imaging;` (1/450 files)
- `using System.Globalization;` (1/450 files)
- `using System.Net;` (1/450 files)
- `using System.Reflection;` (1/450 files)
- `using System.Web;` (1/450 files)

## Common Code Pattern

Most files follow this pattern:

```csharp
using (Document doc = new Document("input.pdf"))
{
    // ... operations ...
    doc.Save("output.pdf");
}
```

## Files in this folder

| File | Title | Key APIs | Description |
|------|-------|----------|-------------|
| [add-a-calculated-field-that-sums-numeric-fields-us...](./add-a-calculated-field-that-sums-numeric-fields-using-javascript-to-display-a-running-total.cs) | Add A Calculated Field That Sums Numeric Fields Using Javasc... | `JavascriptAction` | Add A Calculated Field That Sums Numeric Fields Using Javascript To Display A Running Total |
| [add-a-date-picker-field-that-defaults-to-the-curre...](./add-a-date-picker-field-that-defaults-to-the-current-system-date-upon-pdf-opening.cs) | Add A Date Picker Field That Defaults To The Current System ... | `Rectangle` | Add A Date Picker Field That Defaults To The Current System Date Upon Pdf Opening |
| [add-a-date-picker-field-to-a-pdf-using-javascript-...](./add-a-date-picker-field-to-a-pdf-using-javascript-to-let-users-select-dates-from-a-calendar.cs) | Add A Date Picker Field To A Pdf Using Javascript To Let Use... | `JavascriptAction` | Add A Date Picker Field To A Pdf Using Javascript To Let Users Select Dates From A Calendar |
| [add-a-field-that-calculates-and-displays-the-total...](./add-a-field-that-calculates-and-displays-the-total-price-based-on-quantity-and-unit-price-fields.cs) | Add A Field That Calculates And Displays The Total Price Bas... | `JavascriptAction` | Add A Field That Calculates And Displays The Total Price Based On Quantity And Unit Price Fields |
| [add-a-field-that-displays-the-current-date-and-tim...](./add-a-field-that-displays-the-current-date-and-time-updating-automatically-on-each-view.cs) | Add A Field That Displays The Current Date And Time Updating... | `JavaScriptAction` | Add A Field That Displays The Current Date And Time Updating Automatically On Each View |
| [add-a-hidden-field-that-stores-the-user-s-ip-addre...](./add-a-hidden-field-that-stores-the-user-s-ip-address-captured-via-javascript-on-form-load.cs) | Add A Hidden Field That Stores The User S Ip Address Capture... | `JavascriptAction` | Add A Hidden Field That Stores The User S Ip Address Captured Via Javascript On Form Load |
| [add-a-hidden-field-to-a-pdf-that-stores-a-session-...](./add-a-hidden-field-to-a-pdf-that-stores-a-session-identifier-for-backend-tracking-purposes.cs) | Add A Hidden Field To A Pdf That Stores A Session Identifier... | `Rectangle` | Add A Hidden Field To A Pdf That Stores A Session Identifier For Backend Tracking Purposes |
| [add-a-javascript-validation-script-to-the-email-fi...](./add-a-javascript-validation-script-to-the-email-field-that-checks-for-character-presence.cs) | Add A Javascript Validation Script To The Email Field That C... | `JavascriptAction` | Add A Javascript Validation Script To The Email Field That Checks For Character Presence |
| [add-a-multiline-text-field-called-feedback-with-a-...](./add-a-multiline-text-field-called-feedback-with-a-500-character-limit-to-capture-detailed-user-comments.cs) | Add A Multiline Text Field Called Feedback With A 500 Charac... | `Rectangle` | Add A Multiline Text Field Called Feedback With A 500 Character Limit To Capture Detailed User Co... |
| [add-a-new-blank-page-to-an-existing-pdf-form-while...](./add-a-new-blank-page-to-an-existing-pdf-form-while-preserving-existing-fields.cs) | Add A New Blank Page To An Existing Pdf Form While Preservin... |  | Add A New Blank Page To An Existing Pdf Form While Preserving Existing Fields |
| [add-a-new-checkbox-field-named-agreeterms-to-an-ex...](./add-a-new-checkbox-field-named-agreeterms-to-an-existing-pdf-via-document.form.add.cs) | Add A New Checkbox Field Named Agreeterms To An Existing Pdf... | `Rectangle` | Add A New Checkbox Field Named Agreeterms To An Existing Pdf Via Document.Form.Add |
| [add-a-new-page-to-a-pdf-and-place-fresh-acroform-f...](./add-a-new-page-to-a-pdf-and-place-fresh-acroform-fields-on-that-page.cs) | Add A New Page To A Pdf And Place Fresh Acroform Fields On T... | `FormEditor` | Add A New Page To A Pdf And Place Fresh Acroform Fields On That Page |
| [add-a-progress-bar-field-that-updates-as-the-user-...](./add-a-progress-bar-field-that-updates-as-the-user-completes-sections-of-the-form.cs) | Add A Progress Bar Field That Updates As The User Completes ... | `JavascriptAction` | Add A Progress Bar Field That Updates As The User Completes Sections Of The Form |
| [add-a-reset-button-that-clears-all-user-entered-da...](./add-a-reset-button-that-clears-all-user-entered-data-from-the-form-fields-when-clicked.cs) | Add A Reset Button That Clears All User Entered Data From Th... | `FormEditor` | Add A Reset Button That Clears All User Entered Data From The Form Fields When Clicked |
| [add-a-reset-button-to-a-pdf-form-that-clears-all-u...](./add-a-reset-button-to-a-pdf-form-that-clears-all-user-entered-data-and-restores-defaults.cs) | Add A Reset Button To A Pdf Form That Clears All User Entere... | `FormEditor` | Add A Reset Button To A Pdf Form That Clears All User Entered Data And Restores Defaults |
| [add-a-signature-field-named-clientsignature-and-se...](./add-a-signature-field-named-clientsignature-and-set-its-appearance-to-a-predefined-image-stamp.cs) | Add A Signature Field Named Clientsignature And Set Its Appe... | `ImageStamp` | Add A Signature Field Named Clientsignature And Set Its Appearance To A Predefined Image Stamp |
| [add-a-signature-field-to-a-pdf-form-to-allow-users...](./add-a-signature-field-to-a-pdf-form-to-allow-users-to-apply-handwritten-digital-signatures.cs) | Add A Signature Field To A Pdf Form To Allow Users To Apply ... | `Rectangle` | Add A Signature Field To A Pdf Form To Allow Users To Apply Handwritten Digital Signatures |
| [add-a-submit-button-to-a-pdf-form-that-posts-all-f...](./add-a-submit-button-to-a-pdf-form-that-posts-all-field-data-to-a-specified-url-endpoint.cs) | Add A Submit Button To A Pdf Form That Posts All Field Data ... | `FormEditor` | Add A Submit Button To A Pdf Form That Posts All Field Data To A Specified Url Endpoint |
| [add-a-tooltip-to-a-form-field-providing-guidance-o...](./add-a-tooltip-to-a-form-field-providing-guidance-on-the-expected-input-format.cs) | Add A Tooltip To A Form Field Providing Guidance On The Expe... |  | Add A Tooltip To A Form Field Providing Guidance On The Expected Input Format |
| [add-a-tooltip-to-the-submit-button-explaining-requ...](./add-a-tooltip-to-the-submit-button-explaining-required-fields-before-submission.cs) | Add A Tooltip To The Submit Button Explaining Required Field... | `FormEditor` | Add A Tooltip To The Submit Button Explaining Required Fields Before Submission |
| [add-auto-updating-date-time-field-to-pdf](./add-auto-updating-date-time-field-to-pdf.cs) | Add Auto‑Updating Date/Time Field to PDF | `Document`, `Rectangle`, `DateField` | Demonstrates inserting a DateField into a PDF and attaching JavaScript so the field displays the ... |
| [add-blank-page-to-pdf-form](./add-blank-page-to-pdf-form.cs) | Add Blank Page to PDF Form While Preserving Fields | `Document`, `Add`, `Save` | Shows how to load an existing PDF form, insert a new blank page, and save the document, keeping a... |
| [add-calculated-total-field-javascript](./add-calculated-total-field-javascript.cs) | Add Calculated Total Field Using JavaScript | `Document`, `Save`, `Page` | Demonstrates how to create numeric form fields in a PDF and add a calculated field that sums them... |
| [add-calculated-total-price-field](./add-calculated-total-price-field.cs) | Add Calculated Total Price Field to PDF Form | `Document`, `Rectangle`, `NumberField` | Demonstrates how to add numeric fields to a PDF form and use JavaScript to automatically calculat... |
| [add-current-time-field-to-pdf](./add-current-time-field-to-pdf.cs) | Add Current Time Field to PDF on Load | `Document`, `Page`, `Rectangle` | Demonstrates how to create a DateField in a PDF, set its display format to HH:mm:ss, assign the c... |
| [add-custom-image-to-pdf-button](./add-custom-image-to-pdf-button.cs) | Add Custom Image Appearance to PDF Button Field | `Document`, `Save`, `ButtonField` | Demonstrates how to import an image file and set it as the appearance stream of a button field in... |
| [add-date-field-with-validation](./add-date-field-with-validation.cs) | Add Date Field with MM/DD/YYYY Validation | `Document`, `Page`, `Rectangle` | Shows how to create a PDF, add a DateField, set its display format, and attach a JavaScript OnVal... |
| [add-date-picker-field-to-pdf](./add-date-picker-field-to-pdf.cs) | Add Date Picker Field to PDF | `Document`, `Page`, `Rectangle` | Demonstrates how to insert a date picker form field into a PDF and attach JavaScript to open the ... |
| [add-date-picker-field-to-pdf__v2](./add-date-picker-field-to-pdf__v2.cs) | Add Date Picker Field with Current Date to PDF | `Document`, `Rectangle`, `DateField` | Demonstrates how to insert a DateField into an existing PDF and set its default value to the curr... |
| [add-hidden-creation-timestamp-field-to-pdf](./add-hidden-creation-timestamp-field-to-pdf.cs) | Add Hidden Creation Timestamp Field to PDF | `Document`, `Rectangle`, `TextBoxField` | Demonstrates how to insert a hidden, read‑only text box form field containing the document's crea... |
| ... | | | *and 420 more files* |

## Category Statistics
- Total examples: 450

## Category-Specific Tips

### Key API Surface
- `Aspose.Pdf.Document`
- `Aspose.Pdf.Document.Save`
- `Aspose.Pdf.Facades.Form`
- `Aspose.Pdf.Facades.Form.IsRequiredField`
- `Aspose.Pdf.Facades.FormEditor`
- `Aspose.Pdf.Form`
- `Aspose.Pdf.Form.Delete`
- `Aspose.Pdf.Forms.ComboBoxField`
- `Aspose.Pdf.Forms.ComboBoxField.AddOption`
- `Aspose.Pdf.Forms.Field`
- `Aspose.Pdf.Forms.Form`
- `Aspose.Pdf.Forms.Form.Add`
- `Aspose.Pdf.Forms.FormType`
- `Aspose.Pdf.Forms.TextBoxField`
- `Aspose.Pdf.Page`

### Rules
- Bind a PDF document to a FormEditor using BindPdf({input_pdf}) before performing any form modifications.
- Assign a submit URL to a button field with SetSubmitUrl({field_name}, {url}), where {field_name} is the name of the button and {url} is the target URL.
- Persist the changes by calling Save({output_pdf}) after all form updates are completed.
- Load a PDF document: Document {doc} = new Document({input_pdf});
- Set a tooltip for a form field: (({doc}.Form[{field_name}] as Field).AlternateName = {tooltip_text});

### Warnings
- SetSubmitUrl only works for button fields that are configured as submit buttons; applying it to other field types will have no effect.
- AlternateName is used as the tooltip; its visibility depends on the PDF viewer.
- The field must be cast to Aspose.Pdf.Forms.Field to access the AlternateName property.
- Arabic text may require a font that supports Arabic glyphs; the example does not explicitly set a font, which could affect rendering in some viewers.
- Page indexing in Aspose.Pdf is 1‑based; ensure the page exists before referencing it.

## General Tips
- See parent [agents.md](../agents.md) for:
  - **Boundaries** — Always / Ask First / Never rules for all examples
  - **Common Mistakes** — verified anti-patterns that cause build failures
  - **Domain Knowledge** — cross-cutting API-specific gotchas
  - **Testing Guide** — build and run verification steps
- Review code examples in this folder for working-with-forms patterns

<!-- AUTOGENERATED:START -->
Updated: 2026-04-07 | Run: `20260407_212044_4ffbd1`
<!-- AUTOGENERATED:END -->
