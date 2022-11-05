var ctm = Vue.component('CycleTimeMatrix', {
    props: ['Id', 'value'],
    template: '<div class="col-sm-12 col-md-12 col-lg-12" style="border:1px solid gainsboro;"> \
                <div class="ibox" style="Margin-bottom:0px !important">\
                    <div class="ibox-content no-padding">\
                        <div class="row">\
                            <div style="width:12%">\
                                <small class="stats-label">Machine</small>\
                                <h4>{{ MasterData.EquipmentName }}</h4>\
                            </div>\
                            <div style="width:8%">\
                                <small class="stats-label">From - To</small>\
                                <h4>{{ MasterData.FromOperationNo }} - {{ MasterData.ToOperationNo }}</h4>\
                            </div>\
                            <div style="width:8%">\
                                <small class="stats-label">Number of Default Stops</small>\
                                <h4>{{ MasterData.NoOfDefaultStops }}</h4>\
                            </div>\
                            <div style="width:8%">\
                                <small class="stats-label">Loading Time(min)</small>\
                                <h4>{{ MasterData.LoadingTime /60 }}</h4>\
                            </div>\
                            <div style="width:8%">\
                                <small class="stats-label">Unloading Time(min)</small>\
                                <h4>{{ MasterData.UnloadingTime / 60 }}</h4>\
                            </div>\
                            <div style="width:8%">\
                                <small class="stats-label">Inspection Time(min)</small>\
                                <h4>{{ MasterData.InspectionTime / 60 }}</h4>\
                            </div>\
                            <div style="width:8%">\
                                <small class="stats-label">Natural Gap Between Parts(min)</small>\
                                <h4>{{ MasterData.NaturalGapBetweenParts / 60 }}</h4>\
                            </div>\
                            <div style="width:8%">\
                                <small class="stats-label">Manual Content(min)</small>\
                                <h4>{{ MasterData.ManualContent / 60 }}</h4>\
                            </div>\
                            <div style="width:8%">\
                                <small class="stats-label">Program Name</small>\
                                <h4>{{ MasterData.ProgramName }}</h4>\
                            </div>\
                             <div style="width:8%">\
                                <small class="stats-label">Total Cycle Time(min)</small>\
                                <h4>{{ TotalCycleTime }}</h4>\
                            </div>\
                            <div style="width:5%;text-align:end">\
                                <i class="fa fa-trash" v-on:click="DeletePartNumberData( MasterData.PartNumberCycleTimeId )" style="font-size:20px;  color:red"></i>\
                            </div>\
                            <div style="width:5%;text-align:end">\
                           <i class="fa fa-edit" v-on:click="EditPartNumberData( MasterData.PartNumberCycleTimeId )" style="font-size:20px;  color:red"></i>\
                            </div>\
                        </div>\
                    </div>\
                </div>\
                <div class="form-group row col-md-12 col-lg-12 col-sm-12 no-padding">\
                    <div class="row col-md-11 col-lg-11"> \
                        <h4>Cycle Time Details</h4>\
                    </div>\
                    <div class="row col-1" style="text-align:end"> \
                        <i class="fa fa-chevron-down" v-on:click="toggleShow" style="font-size:20px;"></i>\
                    </div> \
                </div>\
                <hr /> \
                <div class="form-group row col-md-12 col-lg-12" v-if="toggle"> \
                    <input type="hidden" class="form-control" id="PartNumberCycleTimeId" v-model="PartNumberCycleTimeId" /> \
                    <div class="row col-xs-12 col-sm-4 col-md-4 col-lg-4"> \
                        <label class="control-label col-md-7" >Sequence </label> \
                        <div class="col-md-5"> \
                            <input type="number" readonly min="0" class="form-control" id="Sequence" v-model="Sequence" /> \
                        </div> \
                    </div> \
                    <div class="row col-xs-12 col-sm-4 col-md-4 col-lg-4"> \
                        <label class="control-label col-md-7">Running Time(min)</label> \
                        <div class="col-md-5"> \
                            <input type="number" min="0" class="form-control" id="RunningTime" v-model="RunningTime" /> \
                        </div> \
                    </div> \
                    <div class="row col-xs-12 col-sm-4 col-md-4 col-lg-4"> \
                        <label class="control-label col-md-7">Default Stop Time(min)</label> \
                        <div class="col-md-5"> \
                            <input type="number" min="0" class="form-control" id="DefaultStopTime" v-model="DefaultStopTime" /> \
                        </div> \
                    </div> \
                    <div class="row col-xs-12 col-sm-1 col-md-1 col-lg-1"> \
                        <div class="control-label col-md-12" style="text-align:end"> \
                            <input type="button" value="Add" class="btn btn-primary" v-on:click="AddCycleMatrix" /> \
                        </div> \
                    </div> \
                </div> \
                <div class="form-horizontal col-xs-12 col-sm-12" v-if="toggle"> \
                    <table class="table table-stripped table-condensed"> \
                        <tr> \
                            <th>Sequence</th> \
                            <th>Running Time(min)</th> \
                            <th>Default Stop Time(min) </th> \
                            <th></th> \
                        </tr> \
                        <tbody id="tblCycleTimeMatrix"> \
                            <tr v-for="item in Trans"> \
                                <td>{{ item.Sequence }}</td> \
                                <td>{{ item.RunningTime / 60 }}</td> \
                                <td>{{ item.DefaultStopTime / 60 }}</td> \
                                <td> \
                                    <button type="button" class="text-center button btn-xs btn-danger" v-on:click="rowDeleteCycleTimeMatrix(item)"> <em class="fa fa-times danger"></em></button> \
                                </td> \
                            </tr> \
                        </tbody> \
                    </table> \
    </div></div>',

    data: function () {
               return {
            CycleTimeMatrixId: '',
                   PartNumberCycleTimeId: '',
                   Sequence: self.Sequence,
            RunningTime: '',
                   DefaultStopTime: '',
                   TotalCycleTime: '',
            Trans: [],
            MasterData: [],
            toggle: false
        }
        
    },
    methods: {
        toggleShow: function () {
            this.toggle = !this.toggle;
        },

        AddCycleMatrix: function (e) {
           
            var self = this;
            var totalCount = parseInt(self.RunningTime) + parseInt(self.DefaultStopTime);
            self.TotalCycleTime += parseInt(totalCount);
            var strURL = '/Masters/PartNumberCycleTimes/SaveCycleTimeMatrix';
            
             //alert(self.MasterData);
           
            if (self.MasterData.NoOfDefaultStops <= self.Trans.length) {
                swal("Limit Reached!", "Number of default stops limit reached, Unable to add. please delete record and try again", "error");
            }
            else {
                var Obj = {
                    PartNumberCycleTimeId: self.PartNumberCycleTimeId,
                    Sequence: self.Sequence++,
                    RunningTime: self.TimeMinToSec(self.RunningTime),
                    DefaultStopTime: self.TimeMinToSec(self.DefaultStopTime),                    
                };
                $.post(strURL, {
                    cycleTimeMatrix: Obj,
                    TotalCycleTime: parseInt(self.TotalCycleTime) * 60
                }, function (data, status) {
                    if (data > 0) {
                        self.GetCycleTimeMatrixData();

                        //if (self.Trans.length > 0)
                        //{
                        //    this.Sequence = (this.Sequence + 1);

                        //}

                    } else {
                        alert("Add Cycle Time Matrix failed.");
                    }
                });
            }

            this.RunningTime = '';
            this.DefaultStopTime = '';
        },

        rowDeleteCycleTimeMatrix: function (item) {
            debugger;
            var self = this;
            var id = item.CycleTimeMatrixId;
            var removeTime = (parseInt(item.DefaultStopTime) + parseInt(item.RunningTime)) / 60;
            self.TotalCycleTime -= parseInt(removeTime);
            swal({
                title: "Are you sure?",
                text: "Do you want to delete?",
                type: "warning",
                showCancelButton: true,
                closeOnConfirm: false,
                showLoaderOnConfirm: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes, delete it!",
            }).then(function () {
                $.post('/PartNumberCycleTimes/DeleteCycleTimeMatrix', {
                    id: id,
                    TotalCycleTime: parseInt(self.TotalCycleTime) * 60
                },
                    function (data) {
                        if (data) {
                            self.GetCycleTimeMatrixData();
                            swal("Deleted!", "Cycle Time Matrix deleted.", "success");
                        }
                        else {
                            swal("Error!", "Cycle Time Matrix not deleted.", "error");
                        }
                    }
                );
            });
        },

        DeletePartNumberData: function (id) {
            var self = this;
            swal({
                title: "Are you sure?",
                text: "Do you want to delete?",
                type: "warning",
                showCancelButton: true,
                closeOnConfirm: false,
                showLoaderOnConfirm: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes, delete it!",
            }).then(function () {
                $.post('/PartNumberCycleTimes/DeletePartNumberCycleTimes', {
                    id: id
                },
                    function (data) {
                        if (data) {
                            app.GetProcessDetailsData();
                            swal("Deleted!", "Part Number Cycle Times deleted.", "success");
                        }
                        else {
                            swal("Error!", "Part Number Cycle Times not deleted. Delete cycle Time Matrix.", "error");
                        }
                    }
                );
            });
        },
        EditPartNumberData: function (id) {
            var self = this;
            window.location = '/Masters/PartNumberCycleTimes/EditView/' + id;
         
        },

        GetCycleTimeMatrixData: function () {
            var self = this;
            $.get('/PartNumberCycleTimes/GetCycleTimeMatrix', {
                Id: this.PartNumberCycleTimeId
            }, function (data, status) {
                if (status == "success") {
                    self.Trans = data;                    
                    self.Sequence = self.Trans.length + 1;
                } else {
                    alert("Data: " + data + "\nStatus: " + status);
                }
            });
        },

        TimeMinToSec: function (Min) {
            return parseInt(Min) * 60;
        },

        TimeSecToMin: function (Sec) {
            return parseInt(Sec) / 60;
        },
    },

    mounted: function () {
        var self = this;
        self.PartNumberCycleTimeId = this.Id;        
        self.MasterData = this.value;
        self.TotalCycleTime = this.TimeSecToMin(this.value.TotalCycleTime);
        self.GetCycleTimeMatrixData();
    }
});

