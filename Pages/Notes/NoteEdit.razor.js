console.log("loaded script");

window.ytAPIReady = false;
window.playerReady = false;
window.player = null;

export function initializeYouTubePlayer()
{
    console.log("initialize called");
    window.player = null;

    var tag = document.createElement('script');
    tag.src = 'https://www.youtube.com/iframe_api';
    var firstScriptTag = document.getElementsByTagName('script')[0];
    firstScriptTag.parentNode.insertBefore(tag, firstScriptTag);
    
    window.onYouTubeIframeAPIReady = onYouTubeIframeAPIReady;
    window.onPlayerReady = onPlayerReady;
    window.onPlayerStateChange = onPlayerStateChange;
}

export function onYouTubeIframeAPIReady() {
    console.log("iframe ready");
    window.player = new YT.Player('bookmark-video', {
        videoId: '',
        width: "100%",
        height: "100%",
        playerVars: {
            autoplay: 1,
            iv_load_policy: 3
        },
        events: {
            'onReady': window.onPlayerReady,
            'onStateChange': window.onPlayerStateChange
        }
    });
    console.log("player created");
}

export function onPlayerReady(event) {
    //document.getElementById('bookmark-video').style.borderColor = '#FF6D00';
    console.log("player ready");
}

export function onPlayerStateChange(event) {
    console.log("player state chanced");
}

export function seekToTime(timeString) {
    console.log("seek called");
    var timeComponents = timeString.split(':'); // split it at the colons

    var seconds = 0;
    switch (timeComponents.length) {
        case 1: //ss
            seconds = timeComponents[0];
            break;
        case 2: //mm:ss
            seconds = (+timeComponents[0]) * 60 + (+timeComponents[1]);
            break;
        case 3: //hh:mm:ss
            seconds = (+timeComponents[0]) * 60 * 60 + (+timeComponents[1]) * 60 + (+timeComponents[2]);
            break;
    }

    if(window.player)
        window.player.seekTo(seconds);
    else
        console.log("player not found");
}

export function getCurrentVideoTime() {
    if(player != null) {
        console.log(player.getCurrentTime());
        return player.getCurrentTime();
    }else{
        return 0;
    }
}

export function setBlazorPageReference(blazorPageHook) {
    window.blazorPageHook = blazorPageHook;
}

export async function getClipboardImageToPaste() {
    try {
        const clipboardItems = await navigator.clipboard.read();
        for (const clipboardItem of clipboardItems) {
            const imageType = clipboardItem.types.find(type => type.startsWith('image/'))
            const blob = await clipboardItem.getType(imageType);
            const array = new Uint8Array(await blob.arrayBuffer());

            window.fileDataStream = function () {
                return array;
            };

            //Finally, invoke the save file back on our blazor page to come fetch the fileDataStream,
            await window.blazorPageHook.invokeMethodAsync('SaveFile');
        }
    } catch (err) {
        console.error(err.name, err.message);
    }
}