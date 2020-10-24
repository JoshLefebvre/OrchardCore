using System;
using System.Threading.Tasks;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCore.ContentTypes.Editors;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Html.Models;
using OrchardCore.Html.ViewModels;

namespace OrchardCore.Html.Settings
{
    public class HtmlBodyPartSettingsDisplayDriver : ContentTypePartDefinitionDisplayDriver
    {
        public override IDisplayResult Edit(ContentTypePartDefinition contentTypePartDefinition, IUpdateModel updater)
        {
            if (!String.Equals(nameof(HtmlBodyPart), contentTypePartDefinition.PartDefinition.Name))
            {
                return null;
            }

            return Initialize<HtmlBodyPartSettingsViewModel>("HtmlBodyPartSettings_Edit", model =>
            {
                var settings = contentTypePartDefinition.GetSettings<HtmlBodyPartSettings>();

                model.SanitizeHtml = settings.SanitizeHtml;
            })
            .Location("Content:20");
        }

        public override async Task<IDisplayResult> UpdateAsync(ContentTypePartDefinition contentTypePartDefinition, UpdateTypePartEditorContext context)
        {
            if (!String.Equals(nameof(HtmlBodyPart), contentTypePartDefinition.PartDefinition.Name))
            {
                return null;
            }

            var model = new HtmlBodyPartSettingsViewModel();
            var settings = new HtmlBodyPartSettings();

            if (await context.Updater.TryUpdateModelAsync(model, Prefix))
            {
                settings.SanitizeHtml = model.SanitizeHtml;

                context.Builder.WithSettings(settings);
            }

            return Edit(contentTypePartDefinition, context.Updater);
        }
    }
}
