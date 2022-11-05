Vue.component('select2', {
    props: ['options', 'value', 'onchange'],
    template: ' <select><slot></slot></select>',
    mounted: function () {
        var vm = this
      var obj=  $(this.$el)
            // init select2
            .select2(this.options)
            .val(this.value)
            .trigger('change')
            // emit event on change.
            .on('change', function () {
                vm.$emit('input', this.value)
            }).on("select2:unselecting", function (e) {
                $(this).data('state', 'unselected');
            }).on("select2:open", function (e) {
                if ($(this).data('state') === 'unselected') {
                    $(this).removeData('state');
                    var self = $(this);
                    setTimeout(function () {
                        self.select2('close');
                        vm.value = "";
                    }, 1);
                }
          }).on("change", this.onchange)
    },
    watch: {
        value: function (value) {
            // update value
            $(this.$el)
                .val(value)
                .trigger('change')
        },
        options: function (options) {
            // update options
            $(this.$el).empty().select2({ data: options })
        }
    },
    destroyed: function () {
        $(this.$el).off().select2('destroy')
    }
});
