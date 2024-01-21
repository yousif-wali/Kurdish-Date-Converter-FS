open System
open System.Globalization

let kurdishMonths = 
    [|"نەورۆز"; "گوڵان"; "جۆزەردان"; "پوشپەڕ"; "گەلاوێژ"; "خەرمانان"; 
      "ڕەزبەر"; "گەڵاڕێزان"; "سەرماوەرز"; "بەفرانبار"; "ڕیبەندان"; "ڕەشەمێ"|]

let convertGregorianToKurdish (localDate: DateTime) =
    let isLeapYear = DateTime.IsLeapYear(localDate.Year)
    let kurdishNewYear = DateTime(localDate.Year, 3, 21)
    let daysInYear = if isLeapYear then 366 else 365

    let daysDifference = int (localDate - kurdishNewYear).TotalDays
    let daysDifferenceAdjusted = 
        if daysDifference < 0 then daysInYear + daysDifference else daysDifference

    let mutable calculateDate = daysDifferenceAdjusted
    let mutable month = 0
    let mutable found = false

    while not found do
        if month <= 5 && (calculateDate - 31 > 0) then
            calculateDate <- calculateDate - 31
            month <- month + 1
        elif month < 11 && (calculateDate - 30 > 0) then
            calculateDate <- calculateDate - 30
            month <- month + 1
        elif month = 11 then
            let twelfthMonthDays = if isLeapYear then 30 else 29
            if calculateDate - twelfthMonthDays > 0 then
                calculateDate <- calculateDate - twelfthMonthDays
                month <- month + 1
            else
                found <- true
        else
            found <- true

    let kurdishYear = 
        if month > 9 || month < 3 then
            localDate.Year + 700
        else
            localDate.Year + 699

    sprintf "%d, %d %s" kurdishYear calculateDate kurdishMonths.[month]

let getDate() =
    convertGregorianToKurdish DateTime.Now

// Example usage
printfn "%s" (getDate())
