<script lang="ts">
    import { Feed } from "stwui";
    import { formatDate } from "stwui/utils";

    const check = "svg-path";
    const currency_usd = "svg-path";

    interface Item {
        avatar?: string;
        icon?: string;
        content: string;
        date: Date;
    }

    const items = [
        {
            avatar: "https://images.unsplash.com/photo-1472099645785-5658abf4ff4e?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=2&w=256&h=256&q=80",
            content:
                "<span>Played a match against Front End Developer and won</span>",
            date: new Date(2022, 8, 21),
        },
        {
            icon: currency_usd,
            content:
                "<span>Challenged by Bethany Blake for a match and emerged victorious</span>",
            date: new Date(2022, 8, 19),
        },
        {
            icon: check,
            content:
                "<span>Competed in a match with Martha Gardner and emerged as the winner</span>",
            date: new Date(2022, 8, 15),
        },
        {
            icon: currency_usd,
            content:
                "<span>Accepted the challenge from Bethany Blake and won the match</span>",
            date: new Date(2022, 8, 2),
        },
        {
            icon: check,
            content:
                "<span>Played a match against Katherine Snyder and emerged victorious</span>",
            date: new Date(2022, 7, 22),
        },
    ];
</script>

<Feed>
    {#each items as item}
        {#if item.avatar}
            <Feed.Item>
                <Feed.Item.Leading slot="leading">
                    <Feed.Item.Leading.Avatar slot="avatar" src={item.avatar} />
                </Feed.Item.Leading>
                <Feed.Item.Content slot="content">
                    {@html item.content}
                </Feed.Item.Content>
                <Feed.Item.DateTime slot="datetime">
                    <time datetime={item.date.toISOString()}
                        >{formatDate(item.date)}</time
                    >
                </Feed.Item.DateTime>
            </Feed.Item>
        {:else if item.icon}
            <Feed.Item>
                <Feed.Item.Leading slot="leading" class="bg-info-background">
                    <Feed.Item.Leading.Icon slot="icon" data={item.icon} />
                </Feed.Item.Leading>
                <Feed.Item.Content slot="content">
                    {@html item.content}
                </Feed.Item.Content>
                <Feed.Item.DateTime slot="datetime">
                    <time datetime={item.date.toISOString()}
                        >{formatDate(item.date)}</time
                    >
                </Feed.Item.DateTime>
            </Feed.Item>
        {:else}
            <Feed.Item>
                <Feed.Item.Content slot="content">
                    {@html item.content}
                </Feed.Item.Content>
                <Feed.Item.DateTime slot="datetime">
                    <time datetime={item.date.toISOString()}
                        >{formatDate(item.date)}</time
                    >
                </Feed.Item.DateTime>
            </Feed.Item>
        {/if}
    {/each}
</Feed>
