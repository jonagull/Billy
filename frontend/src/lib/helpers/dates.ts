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
