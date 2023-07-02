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
                '<span>Played a match against <a href="#player1" class="font-bold">Front End Developer</a> and won</span>',
            date: new Date(2022, 8, 21),
        },
        {
            icon: currency_usd,
            content:
                '<span>Challenged by <a href="#player2" class="font-bold">Bethany Blake</a> for a match and emerged victorious</span>',
            date: new Date(2022, 8, 19),
        },
        {
            icon: check,
            content:
                '<span>Competed in a match with <a href="#player3" class="font-bold">Martha Gardner</a> and emerged as the winner</span>',
            date: new Date(2022, 8, 15),
        },
        {
            icon: currency_usd,
            content:
                '<span>Accepted the challenge from <a href="#player4" class="font-bold">Bethany Blake</a> and won the match</span>',
            date: new Date(2022, 8, 2),
        },
        {
            icon: check,
            content:
                '<span>Played a match against <a href="#player5" class="font-bold">Katherine Snyder</a> and emerged victorious</span>',
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
