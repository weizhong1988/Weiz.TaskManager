
(function ($, window, document) {
    $(function () {
        var grid = {
            UpdateStatus: function (taskId, status) {
                $.ajax({
                    url: "/Task/UpdateStatus",
                    type: "POST",
                    dateType: "JSON",
                    data: { taskId: taskId, status: status },
                    success: function (data) {
                        if (data.result) {
                            art.dialog({
                                icon: 'succeed',
                                title: 'Tips',
                                content: data.msg,
                                close: function () {
                                    window.location.reload();
                                }
                            });
                        }
                        else {
                            art.dialog({
                                icon: 'warning',
                                title: 'Tips',
                                content: data.msg,
                            });
                        }
                    }
                });
            },

            Require: function () {
                var result = true;
                $(".require").each(function () {
                    var obj = $(this);
                    if (obj.val() == "") {
                        obj.next().show();
                        result = false;
                    }
                    else {
                        obj.next().hide();
                    }
                });
                return result;
            },
            Add: function () {
                $.ajax({
                    url: "/Task/Edit",
                    type: "POST",
                    dateType: "html",
                    success: function (data) {
                        art.dialog({
                            content: data,
                            button: [
                              {
                                  name: '保 存',
                                  callback: function () {
                                      grid.Save("add");
                                      return false;
                                  },
                                  focus: true
                              },
                              {
                                  name: '取 消'
                              }
                            ]
                        });
                    }
                });
            },

            Edit: function (taskId) {
                $.ajax({
                    url: "/Task/Edit",
                    type: "POST",
                    dateType: "html",
                    data: { taskId: taskId },
                    success: function (data) {
                        art.dialog({
                            content: data,
                            button: [
                              {
                                  name: '保 存',
                                  callback: function () {
                                      grid.Save("edit");
                                      return false;
                                  },
                                  focus: true
                              },
                              {
                                  name: '取 消'
                              }
                            ]
                        });
                    }
                });
            },

            Save: function (action) {

                if (!grid.Require()) {
                    return false;
                }
                var taskID = $("#TaskID").val();
                var taskName = $("#TaskName").val();
                var taskParam = $("#TaskParam").val();
                var assemblyName = $("#AssemblyName").val();
                var className = $("#ClassName").val();
                var cronExpressionString = $("#CronExpressionString").val();
                var cronRemark = $("#CronRemark").val();
                var status = $("input[type='Status']:checked").val();

                var json = {
                    TaskID: taskID,
                    TaskName: taskName,
                    TaskParam: taskParam,
                    AssemblyName: assemblyName,
                    ClassName: className,
                    CronExpressionString: cronExpressionString,
                    CronRemark: cronRemark,
                    Status: status
                }

                $.ajax({
                    url: "/Task/Save",
                    type: "post",
                    dataType: "json",
                    data: { data: JSON.stringify(json), action: action },
                    success: function (data) {
                        if (data.result) {
                            art.dialog({
                                icon: 'succeed',
                                title: 'Tips',
                                content: data.msg,
                                close: function () {
                                    this.hide();
                                    window.location.reload();
                                }
                            });

                        }
                        else {
                            art.dialog({
                                icon: 'warning',
                                title: 'Tips',
                                content: data.msg,
                            });
                        }
                    }

                });


            }
        };

        window.grid = grid;
    });
})(jQuery, window);

$(function () {
    var list = $("#content");
    $(list).on('click', 'a', function (e) {
        var the = $(this);
        if (the.is(".updatestatus")) {
            e.preventDefault();
            var id = the.data("id");
            var status = the.data("status");
            grid.UpdateStatus(id, status);
        }
        if (the.is(".edit")) {
            e.preventDefault();
            var id = the.data("id");
            grid.Edit(id);
        }
        if (the.is(".add")) {
            e.preventDefault();
            grid.Add();
        }
    });
});