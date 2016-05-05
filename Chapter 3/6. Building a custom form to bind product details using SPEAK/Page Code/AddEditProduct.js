define(["sitecore"], function (Sitecore) {
    var AddEditProduct = Sitecore.Definitions.App.extend({
        initialized: function () {
            var id = Sitecore.Helpers.url.getQueryParameters(window.location.href)['id'];
            if (Sitecore.Helpers.id.isId(id)) {
                this.getProduct(id);
            }
        },

        getProduct: function (id) {
            var app = this;
            jQuery.ajax({
                type: "GET", dataType: "json",
                url: "/api/sitecore/Product/GetProduct",
                data: { 'id': id }, cache: false,
                success: function (data) {
                    app.fillForm(data);
                },
                error: function () {
                    alert("There was an error. Try again please!");
                }
            });
        },
        fillForm: function (data) {
            var app = this;
            app.TextTitle.set('text', data.Title);
            app.HeaderTitle.set('text', data.Title);
            app.TextPrice.set('text', data.Price);
            app.DateRelease.set('date', data.ReleaseDate);
        }
        
    });

    return AddEditProduct;
});