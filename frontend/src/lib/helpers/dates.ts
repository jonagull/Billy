export const formatDate = (input: Date | string) => {
    return new Date(input).toLocaleDateString("en-GB");
};

export function formatDateTime(dateTimeString: string | number | Date) {
    const dateTime = new Date(dateTimeString);
    const options = { day: "numeric", month: "numeric", year: "numeric" };
    return dateTime.toLocaleDateString(
        "no",
        options as Intl.DateTimeFormatOptions
    );
}

export const formatFeedDate = (time: any) => {
    const date = new Date(time).toLocaleString("no-NO", {
        timeZone: "Europe/Oslo",
    });

    const [datePart, timePart] = date.split(",");

    if (datePart === undefined || timePart === undefined) {
        return;
    }

    const dateAndMonthSplit = datePart.split(".", 2);
    let dateAndMonthTogether = dateAndMonthSplit.join(".");
    const timeSplit = timePart.split(":", 2);
    const timeTogether = timeSplit.join(":");
    const splittedDate = dateAndMonthTogether.split(".");
    const splittedDateLastIndex = splittedDate[1];

    if (splittedDateLastIndex.length === 1) {
        const keksde = "0" + splittedDateLastIndex;
        dateAndMonthTogether = splittedDate[0] + "." + keksde;
    }

    return timeTogether + " - " + dateAndMonthTogether;
};
