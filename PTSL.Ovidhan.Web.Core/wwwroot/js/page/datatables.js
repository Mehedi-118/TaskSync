"use strict";

$("[data-checkboxes]").each(function () {
    var me = $(this),
        group = me.data('checkboxes'),
        role = me.data('checkbox-role');

    me.change(function () {
        var all = $('[data-checkboxes="' + group + '"]:not([data-checkbox-role="dad"])'),
            checked = $('[data-checkboxes="' + group + '"]:not([data-checkbox-role="dad"]):checked'),
            dad = $('[data-checkboxes="' + group + '"][data-checkbox-role="dad"]'),
            total = all.length,
            checked_length = checked.length;

        if (role == 'dad') {
            if (me.is(':checked')) {
                all.prop('checked', true);
            } else {
                all.prop('checked', false);
            }
        } else {
            if (checked_length >= total) {
                dad.prop('checked', true);
            } else {
                dad.prop('checked', false);
            }
        }
    });
});

$("#table-1").dataTable({
    "columnDefs": [
        { "sortable": false, "targets": [2, 3] }
    ]
});
$("#table-2").dataTable({
    "columnDefs": [
        { "sortable": false, "targets": [0, 2, 3] }
    ],
    order: [[1, "asc"]] //column indexes is zero based

});
$('#save-stage').DataTable({
    "scrollX": true,
    stateSave: true
});

$('#tableExport').DataTable({    
    dom: 'lBfrtip',
    //"scrollY": 200,
    //"scrollX": true,
    //buttons: [
    //    'copy', 'csv', 'excel', 'pdf', 'print'
    //],
    buttons: [
        //{
        //    extend: 'copyHtml5',
        //    exportOptions: {
        //       // columns: [0, ':visible']
        //        columns: ':not(:last-child)',
        //    }
        //},
        //{
        //    extend: 'excelHtml5',
        //    exportOptions: {
        //        columns: ':not(:last-child)',
        //        //columns: ':visible'
        //    }
        //},
        //{
        //    extend: 'pdfHtml5',
        //    exportOptions: {
        //        //columns: [0, 1]
        //        //columns: ':visible(:not(.not-export-col))'
        //        columns: ':not(:last-child)',
        //    }
        //},
        //{
        //    extend: 'print',
        //    exportOptions: {
        //        //columns: [0, 1]
        //        //columns: ':visible(:not(.not-export-col))'
        //        columns: ':not(:last-child)',
        //    }
        //},
        //'colvis'
    ],
    //exportOptions: {
    //    columns: ':visible(:not(.not-export-col))'
    //},
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

$('#tableExport_button').DataTable({ 
    dom: 'lBfrtip',
    paging: true,
    ordering: true,
    info: true,
    stateSave: true,
    stateDuration: 0,
    /*scrollX: true,*/
    scrollCollapse: true,
    fixedColumns: true,
    //buttons: [
    //    'copyHtml5',
    //    {
    //        extend: 'excelHtml5',
    //        text: 'Excel',
    //        //bShowAll: true,
    //        exportOptions: {
    //            //columns: ':visible',
    //            columns: ':visible(:not(.not-export-col))',
    //            rows: { selected: true }
    //            //rows: { selected: false }
    //        }
    //    },
    //    'csvHtml5',
    //    'pdfHtml5'
    //],
    buttons: [
        //{
        //    extend: 'copyHtml5',
        //    exportOptions: {
        //        // columns: [0, ':visible']
        //        columns: ':not(:last-child)',
        //    }
        //},
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'pdfHtml5',
            exportOptions: {
                //columns: [0, 1]
                //columns: ':visible(:not(.not-export-col))'
                columns: ':not(:last-child)',
                charset: 'UTF-8'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                //columns: [0, 1]
                //columns: ':visible(:not(.not-export-col))'
                columns: ':not(:last-child)',
            }
        },
        //'colvis'
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20,50,100, 'All']]
});

$('.tableExport').DataTable({
    "bStateSave": true,
    dom: 'lBfrtip',
    //"scrollY": 200,
    //"scrollX": true,
    //buttons: [
    //    'copy', 'csv', 'excel', 'pdf', 'print'
    //],
    buttons: [
        //{
        //    extend: 'copyHtml5',
        //    exportOptions: {
        //        // columns: [0, ':visible']
        //        columns: ':not(:last-child)',
        //    }
        //},
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        //,
        //{
        //    extend: 'pdfHtml5',
        //    exportOptions: {
        //        //columns: [0, 1]
        //        //columns: ':visible(:not(.not-export-col))'
        //        columns: ':not(:last-child)',
        //        charset: 'UTF-8'
        //    }
        //},
        
        //{
        //    "extend": "pdf",
        //    "text": "PDF",
        //    "filename": "Report Name",
        //    "className": "btn btn-green",
        //    "charset": "utf-8"
        //},
        {
            extend: 'print',
            exportOptions: {
                //columns: [0, 1]
                //columns: ':visible(:not(.not-export-col))'
                columns: ':not(:last-child)',
            }
        },
        //'colvis'
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

$('.tableExport_button').DataTable({
    dom: 'lBfrtip',
    paging: true,
    ordering: true,
    info: true,
    stateSave: true,
    stateDuration: 0,
    scrollX: true,
    scrollCollapse: true,
    fixedColumns: true,
    //buttons: [
    //    'copyHtml5',
    //    {
    //        extend: 'excelHtml5',
    //        text: 'Excel',
    //        //bShowAll: true,
    //        exportOptions: {
    //            columns: ':visible',
    //            rows: { selected: true }
    //            //rows: { selected: false }
    //        }
    //    },
    //    'csvHtml5',
    //    'pdfHtml5'
    //],
    buttons: [
        //{
        //    extend: 'copyHtml5',
        //    exportOptions: {
        //        // columns: [0, ':visible']
        //        columns: ':not(:last-child)',
        //    }
        //},
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'pdfHtml5',
            exportOptions: {
                //columns: [0, 1]
                //columns: ':visible(:not(.not-export-col))'
                columns: ':not(:last-child)',
                charset: 'UTF-8'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                //columns: [0, 1]
                //columns: ':visible(:not(.not-export-col))'
                columns: ':not(:last-child)',
            }
        },
        //'colvis'
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});


var presentAddressDatatable = $('.presentAddressDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadpresentAddressDatatable() {
    presentAddressDatatable.destroy();
    presentAddressDatatable = $('.presentAddressDatatable').DataTable({
        dom: 'lBfrtip',        
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}

var permanentAddressDatatable = $('.permanentAddressDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadPermanentAddressDatatable() {
    permanentAddressDatatable.destroy();
    permanentAddressDatatable = $('.permanentAddressDatatable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}

var spouseDatatable = $('.spouseDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadSpouseDatatable() {
    spouseDatatable.destroy();
    spouseDatatable = $('.spouseDatatable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}

var childDatatable = $('.childDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadChildDatatable() {
    childDatatable.destroy();
    childDatatable = $('.childDatatable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}

var educationDatatable = $('.educationDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadEducationDatatable() {
    educationDatatable.destroy();
    educationDatatable = $('.educationDatatable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}

var trainingDatatable = $('.trainingDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadTrainingDatatable() {
    trainingDatatable.destroy();
    trainingDatatable = $('.trainingDatatable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}

var serviceDatatable = $('.serviceDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadServiceDatatable() {
    serviceDatatable.destroy();
    serviceDatatable = $('.serviceDatatable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}

var promotionDatatable = $('.promotionDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadPromotionDatatable() {
    promotionDatatable.destroy();
    promotionDatatable = $('.promotionDatatable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}

var discipilinaryDatatable = $('.discipilinaryDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadDiscipilinaryDatatable() {
    discipilinaryDatatable.destroy();
    discipilinaryDatatable = $('.discipilinaryDatatable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}

var transferDatatable = $('.transferDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadTransferDatatable() {
    transferDatatable.destroy();
    transferDatatable = $('.transferDatatable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}

var languageDatatable = $('.languageDatatable').DataTable({
    dom: 'lBfrtip',
    buttons: [
        {
            extend: 'excelHtml5',
            exportOptions: {
                columns: ':not(:last-child)',
                //columns: ':visible'
            }
        },
        {
            extend: 'print',
            exportOptions: {
                columns: ':not(:last-child)',
            }
        },
    ],
    exportOptions: {
        columns: ':visible(:not(.not-export-col))'
    },
    pageLength: 5,
    lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
});

function LoadLanguageDatatable() {
    languageDatatable.destroy();
    languageDatatable = $('.languageDatatable').DataTable({
        dom: 'lBfrtip',
        buttons: [
            {
                extend: 'excelHtml5',
                exportOptions: {
                    columns: ':not(:last-child)',
                    //columns: ':visible'
                }
            },
            {
                extend: 'print',
                exportOptions: {
                    columns: ':not(:last-child)',
                }
            },
        ],
        exportOptions: {
            columns: ':visible(:not(.not-export-col))'
        },
        pageLength: 5,
        lengthMenu: [[5, 10, 20, 50], [5, 10, 20, 50, 100, 'All']]
    });
}